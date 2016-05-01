using System.Collections.Generic;

namespace Olycksassistenten.Logic
{
    public class Report
    {
        #region Constructors

        public Report(User driver1, User driver2, Vehicle vehicle1, Vehicle vehicle2, InsuranceCompany insuranceCompany1, InsuranceCompany insuranceCompany2, string descriptionAccident)
        {
            Driver1 = driver1;
            Driver2 = driver2;
            Vehicle1 = vehicle1;
            Vehicle2 = vehicle2;
            InsuranceCompany1 = insuranceCompany1;
            InsuranceCompany2 = insuranceCompany2;
            DescriptionAccident = descriptionAccident;
            PhotosAccident = new List<string>();
        }

        public Report(User driver1, User driver2, Vehicle vehicle1, Vehicle vehicle2, InsuranceCompany insuranceCompany1, InsuranceCompany insuranceCompany2, string descriptionAccident, List<string> photosAccident)
        {
            Driver1 = driver1;
            Driver2 = driver2;
            Vehicle1 = vehicle1;
            Vehicle2 = vehicle2;
            InsuranceCompany1 = insuranceCompany1;
            InsuranceCompany2 = insuranceCompany2;
            DescriptionAccident = descriptionAccident;
            PhotosAccident = photosAccident;
        }

        #endregion

        #region Properties

        public User Driver1 { get; private set; }
        public User Driver2 { get; private set; }
        public Vehicle Vehicle1 { get; private set; }
        public Vehicle Vehicle2 { get; private set; }
        public InsuranceCompany InsuranceCompany1 { get; private set; }
        public InsuranceCompany InsuranceCompany2 { get; private set; }
        public string DescriptionAccident { get; private set; }
        public List<string> PhotosAccident { get; private set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            var str ="SKADEANM�LAN MED BESKRIVNING AV H�NDELSEF�RLOPPET\n\n" +

                     "F�RARE 1 - RegistrationNr1 Model1 Year1:\n\n" +

                     "Namn: FullName1\n" +
                     "Personnummer: Ssn1\n" +
                     "Adress: Address1\n" +
                     "Telefon: Phone1\n" +
                     "Mail: Email1\n" +
                     "F�rs�kringsbolag: InsuranceCompany1\n\n" +

                     "F�RARE 2 - RegistrationNr2 Model2 Year2:\n\n" +

                     "Namn: FullName2\n" +
                     "Personnummer: Ssn2\n" +
                     "Adress: Address2\n" +
                     "Telefon: Phone2\n" +
                     "Mail: Email2\n" +
                     "F�rs�kringsbolag: InsuranceCompany2\n\n" +

                     "BESKRIVNING AV H�NDELSEN:\n" +
                     "\"DescriptionAccident\"\n\n" +

                     "SE BIFOGADE BILDER FR�N SKADETILLF�LLET\n\n" +

                     "V�nliga h�lsningar,\n" +
                     "FullName1 och FullName2\n\n" +

                     "OBS:\nDetta mail �r skapat med Olycksassistenten och b�da parter har givit sitt godk�nnande till h�ndelsef�rloppet,\n" +
                     "i och med att b�da parter har identifierats via sitt k�rkort. En kopia p� denna anm�lan har skickats till b�da\n" +
                     "parters f�rs�kringsbolag f�r vidare behandling";

            //ers�tt info f�rare 1
            str = str.Replace("RegistrationNr1", Vehicle1.RegistrationNumber);
            str = str.Replace("Model1", Vehicle1.Model);
            str = str.Replace("Year1", Vehicle1.Year.ToString());
            str = str.Replace("FullName1", Driver1.FullName);
            str = str.Replace("Ssn1", Driver1.Ssn.ToString());
            str = str.Replace("Address1", Driver1.Address);
            str = str.Replace("Phone1", Driver1.Phone);
            str = str.Replace("Email1", Driver1.Email);
            str = str.Replace("InsuranceCompany1", InsuranceCompany1.CompanyName);

            //ers�tt info f�rare 2
            str = str.Replace("RegistrationNr2", Vehicle2.RegistrationNumber);
            str = str.Replace("Model2", Vehicle2.Model);
            str = str.Replace("Year2", Vehicle2.Year.ToString());
            str = str.Replace("FullName2", Driver2.FullName);
            str = str.Replace("Ssn2", Driver2.Ssn.ToString());
            str = str.Replace("Address2", Driver2.Address);
            str = str.Replace("Phone2", Driver2.Phone);
            str = str.Replace("Email2", Driver2.Email);
            str = str.Replace("InsuranceCompany2", InsuranceCompany2.CompanyName);

            //ers�tt beskrivning av skadan
            str = str.Replace("DescriptionAccident", DescriptionAccident);

            return str;
        }

        #endregion
    }
}