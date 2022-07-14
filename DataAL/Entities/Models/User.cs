

namespace DataAL.Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string PrivateNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string Address { get; set; }
        public Country Country { get; set; }
        public Role Role { get; set; }
        public Gender Gender { get; set; }
        public int Active { get; set; } = 1;

    }
}
