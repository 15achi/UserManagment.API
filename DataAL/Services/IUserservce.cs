
using DataAL.Entities.Models;
using DataAL.Entities.UserDbModel;
using DataAL.Page;

namespace DataAL.Services
{
    public interface IUserservce
    {
        PagedList<UsersGetDto> GetUsers(UserParameters userParameters);
        Country GetCountryById(UserDto userDto);
        Country GetCountryByUserEditDtoId(UserEditDto userEditDto);
        User GetUserByPrivateNumber(string privatenumber);
        UsersGetDto UsersGetDtoByPrivateNumber(string privatenumber);
        public string CreatePasswordHash(string password);
        bool EditUser(string privatenumber, UserEditDto userEditDto);
        bool DeleteUser(string privatenumber);
        bool PassingTheUser(string privatenumber);
        bool ActivationOfUser(string privatenumber);
        bool ExistsUserEmail(UserDto userDto);
        bool ExistsUserPhone(UserDto userDto);
        bool ExistsUserPrivateNumber(UserDto userDto);
        bool ExistsCountryId(UserDto userDto);
        bool AddUser(UserDto userDto);
        bool UserExists(string privatenumber);
        bool Save();
    }
}
