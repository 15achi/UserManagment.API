using DataAL.Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace DataAL.Entities.UserDbModel
{
    public record UserDto
    {

        public string PrivateNumber { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

       // public string ConfirmPassword { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public string Address { get; set; }

        [EnumDataType(typeof(Gender))]
        public string GenderType { get; set; }

        [EnumDataType(typeof(Role))]
        public string RoleType { get; set; }

        public int CountryId { get; set; }


    }
}
