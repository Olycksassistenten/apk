namespace Olycksassistenten.Logic
{
    public class Vehicle
    {
        public Vehicle(string registrationNumber, string model, int year)
        {
            RegistrationNumber = registrationNumber.ToUpper();
            Model = model.ToUpper();
            Year = year;
        }

        public string RegistrationNumber { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
}