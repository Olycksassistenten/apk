using SQLite;

namespace Olycksassistenten.Logic
{
    [Table("User")]
    public class User
    {
        public User() { }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public long Ssn { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}