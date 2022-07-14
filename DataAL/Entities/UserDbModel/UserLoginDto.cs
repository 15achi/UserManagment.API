

namespace DataAL.Entities.UserDbModel
{
    public record UserLoginDto
    {
        public string PrivateNumber { get; set; }

        public string Password { get; set; }
    }
}
