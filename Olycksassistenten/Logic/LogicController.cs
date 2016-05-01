using System.Collections.Generic;
using System.Linq;
using Olycksassistenten.Data;
using Olycksassistenten.Helpers;

namespace Olycksassistenten.Logic
{
    public class LogicController
    {
        #region Fields

        private InsuranceCompanyRepository _insuranceCompanyRepository;
        private UserRepository _userRepository;

        #endregion

        #region Constructors

        public LogicController()
        {
            _insuranceCompanyRepository = new InsuranceCompanyRepository();
            _userRepository = new UserRepository();
        }

        #endregion

        #region Methods

        public List<InsuranceCompany> GetAllInsuranceCompanies()
        {
            return _insuranceCompanyRepository.GetAll();
        }

        public bool IsUsernameTaken(string username)
        {
            return _userRepository.Find(x => x.Username.ToLower() == username).Any();
        }

        public bool IsEmailTaken(string email)
        {
            return _userRepository.Find(x => x.Email.ToLower() == email).Any();
        }

        public void InsertOrUpdate(User user)
        {
            _userRepository.InsertOrUpdate(user);
        }

        public User Login(string username, string password)
        {
            return _userRepository.Find(x => x.Username == username && x.Password == password).FirstOrDefault();
        }

        public bool ForgotPassword(string email)
        {
            var user = _userRepository.Find(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
            if (user != null)
            {
                var subject = "P�minnelse av l�senord";
                var body = string.Format("Ditt anv�ndarnamn �r: {0}\nDitt l�senord �r: {1}", user.Username, user.Password);

                MailHelper.SendGmail(email, subject, body);
                return true;
            }
            return false;
        }

        public User GetUser(string username)
        {
            return _userRepository.Find(x => x.Username == username).FirstOrDefault();
        }

        public User Lookup(string ssn)
        {
            //anv�nd mockad data tillf�lligt
            //h�r �r det t�nkt att jag ska sl� upp info utifr�n personnummer
            //dock har jag valt att avgr�nsa mig fr�n att implementera detta i version ett av applikationen
            return new User
            {
                Username = "mock",
                Password = "mock", 
                FullName = "Kalle Andersson", 
                Address = "�ngsstigen 6B", 
                Ssn = 8309055551, 
                Email = "caroline.larsson@mail.com", 
                Phone = "07336503128"
            };
        }

        public void SubmitInsuranceClaims(Report report)
        {
            //s�nd till f�rs�kringsbolag 1, cc f�rare 1
            var subject1 = string.Format("Skadeanm�lan {0}", report.Vehicle1.RegistrationNumber);
            MailHelper.SendGmail(report.InsuranceCompany1.Email, subject1, report.ToString(), report.Driver1.Email, report.PhotosAccident);

            //s�nd till f�rs�kringsbolag 2, cc f�rare 2
            var subject2 = string.Format("Skadeanm�lan {0}", report.Vehicle2.RegistrationNumber);
            MailHelper.SendGmail(report.InsuranceCompany2.Email, subject2, report.ToString(), report.Driver2.Email, report.PhotosAccident);
        }

        #endregion
    }
}