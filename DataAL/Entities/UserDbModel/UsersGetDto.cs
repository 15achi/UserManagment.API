

namespace DataAL.Entities.UserDbModel
{
    public record UsersGetDto
    {
        public string PrivateNumber { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public string Address { get; set; }

        public string GenderType { get; set; }

        public string RoleType { get; set; }

        public string Country { get; set; }

        public int CountryId { get; set; }
    }
}
