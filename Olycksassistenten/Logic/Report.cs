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
            var str ="SKADEANMÄLAN MED BESKRIVNING AV HÄNDELSEFÖRLOPPET\n\n" +

                     "FÖRARE 1 - RegistrationNr1 Model1 Year1:\n\n" +

                     "Namn: FullName1\n" +
                     "Personnummer: Ssn1\n" +
                     "Adress: Address1\n" +
                     "Telefon: Phone1\n" +
                     "Mail: Email1\n" +
                     "Försäkringsbolag: InsuranceCompany1\n\n" +

                     "FÖRARE 2 - RegistrationNr2 Model2 Year2:\n\n" +

                     "Namn: FullName2\n" +
                     "Personnummer: Ssn2\n" +
                     "Adress: Address2\n" +
                     "Telefon: Phone2\n" +
                     "Mail: Email2\n" +
                     "Försäkringsbolag: InsuranceCompany2\n\n" +

                     "BESKRIVNING AV HÄNDELSEN:\n" +
                     "\"DescriptionAccident\"\n\n" +

                     "SE BIFOGADE BILDER FRÅN SKADETILLFÄLLET\n\n" +

                     "Vänliga hälsningar,\n" +
                     "FullName1 och FullName2\n\n" +

                     "OBS:\nDetta mail är skapat med Olycksassistenten och båda parter har givit sitt godkännande till händelseförloppet,\n" +
                     "i och med att båda parter har identifierats via sitt körkort. En kopia på denna anmälan har skickats till båda\n" +
                     "parters försäkringsbolag för vidare behandling";

            //ersätt info förare 1
            str = str.Replace("RegistrationNr1", Vehicle1.RegistrationNumber);
            str = str.Replace("Model1", Vehicle1.Model);
            str = str.Replace("Year1", Vehicle1.Year.ToString());
            str = str.Replace("FullName1", Driver1.FullName);
            str = str.Replace("Ssn1", Driver1.Ssn.ToString());
            str = str.Replace("Address1", Driver1.Address);
            str = str.Replace("Phone1", Driver1.Phone);
            str = str.Replace("Email1", Driver1.Email);
            str = str.Replace("InsuranceCompany1", InsuranceCompany1.CompanyName);

            //ersätt info förare 2
            str = str.Replace("RegistrationNr2", Vehicle2.RegistrationNumber);
            str = str.Replace("Model2", Vehicle2.Model);
            str = str.Replace("Year2", Vehicle2.Year.ToString());
            str = str.Replace("FullName2", Driver2.FullName);
            str = str.Replace("Ssn2", Driver2.Ssn.ToString());
            str = str.Replace("Address2", Driver2.Address);
            str = str.Replace("Phone2", Driver2.Phone);
            str = str.Replace("Email2", Driver2.Email);
            str = str.Replace("InsuranceCompany2", InsuranceCompany2.CompanyName);

            //ersätt beskrivning av skadan
            str = str.Replace("DescriptionAccident", DescriptionAccident);

            return str;
        }

        #endregion
    }
}