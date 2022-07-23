using DataAL.Entities.Models;
using DataAL.Entities.UserDbModel;
using DataAL.Page;


namespace UserManagement.BLL.Models.Users
{
    public interface IUserService
    {
        PagedList<UsersGetDto> GetUsers(UserParameters userParameters);

        List<UsersGetDto> GetPassingUsers();
        User GetUserByPrivateNumber(string privatenumber);
        UsersGetDto UsersGetDtoByPrivateNumber(string privatenumber);
        public string CreatePasswordHash(string password);
        void AddUser(UserDto userDto);
        void EditUser(string privatenumber, UserEditDto userEditDto);
        bool PassingTheUser(string privatenumber);
        bool ActivationOfUser(string privatenumber);
        bool DeleteUser(string privatenumber);
        bool UserExists(string privatenumber);
    }
}
