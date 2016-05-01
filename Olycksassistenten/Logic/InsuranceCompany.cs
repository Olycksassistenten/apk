using SQLite;

namespace Olycksassistenten.Logic
{
    [Table("InsuranceCompany")]
    public class InsuranceCompany
    {
        public InsuranceCompany() { }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public string CompanyName { get; set; }
        public string Email { get; set; }
    }
}