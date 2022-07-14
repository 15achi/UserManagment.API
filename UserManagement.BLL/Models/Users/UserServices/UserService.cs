using DataAL.Entities.Models;
using DataAL.Entities.UserDbModel;
using DataAL.Page;
using DataAL.Services;
using UserManagment.API.ErrorMiddleware;

namespace UserManagement.BLL.Models.Users
{
    public class UserService : IUserService
    {
        private readonly IUserservce _userservce;
        public UserService(IUserservce userservce)
        {
            _userservce = userservce;
        }
        public void AddUser(UserDto userDto)
        {
            _userservce.AddUser(AppExceptionOrUserDto(userDto));
        }

        public void EditUser(string privatenumber, UserEditDto userEditDto)
        {
            if (privatenumber.Trim() != userEditDto.PrivateNumber.Trim())
            {
                throw new AppException("გადაცემული პირადი ნომრი'" + privatenumber + "'ტოლი უნდა იყოს '" +
                    userEditDto.PrivateNumber + "'" + " ის");
            }
            else
            {
                _userservce.EditUser(AppExceptionOrPrivatenumber(privatenumber),AppExceptionOrUserEditDto(userEditDto));
            }
        }

        public bool DeleteUser(string privatenumber)
        {
            return _userservce.DeleteUser(AppExceptionOrPrivatenumber(privatenumber));
        }

        public bool PassingTheUser(string privatenumber)
        {
            return _userservce.PassingTheUser(AppExceptionOrPrivatenumber(privatenumber));
        }

        public bool ActivationOfUser(string privatenumber)
        {
            return _userservce.ActivationOfUser(AppExceptionOrPrivatenumber(privatenumber));
        }

        public string CreatePasswordHash(string password)
        {
            return _userservce.CreatePasswordHash(password);
        }

        public User GetUserByPrivateNumber(string privatenumber)
        {
            return _userservce.GetUserByPrivateNumber(AppExceptionOrPrivatenumber(privatenumber));
        }

        public UsersGetDto UsersGetDtoByPrivateNumber(string privatenumber)
        {         
            return _userservce.UsersGetDtoByPrivateNumber(AppExceptionOrPrivatenumber(privatenumber));
        }

        public PagedList<UsersGetDto> GetUsers(UserParameters userParameters)
        {
            return _userservce.GetUsers(userParameters);
        }

        public bool UserExists(string privatenumber)
        {
            return _userservce.UserExists(privatenumber.Trim());
        }

        public bool IsValidNamber(string Namber)
        {
            return Namber.All(char.IsNumber);
        }


        public string AppExceptionOrPrivatenumber(string privatenumber)
        {
            if (!IsValidNamber(privatenumber.Trim()))
            {
                throw new AppException("პირადი ნომრი'" + privatenumber + "' უნდა შედგებოდეს ციფრებისგან");
            }
            else if (privatenumber.Length != 11)
            {
                throw new AppException("პირადი ნომრი'" + privatenumber + "' უნდა იყოს 11 ციფრი");
            }
            else if (!UserExists(privatenumber.Trim()))
            {
                throw new AppException("User-ი '" + privatenumber + "' პირადი ნომრით არ არსებობს");
            }
            else
            {
                return privatenumber.Trim();
            }
        }

        public UserDto AppExceptionOrUserDto(UserDto userDto)
        {
            if (_userservce.ExistsUserEmail(userDto))
            {
                throw new AppException("User-ი ამ -'" + userDto.Email + "'-ით უკვე არსებობს");
            }
            else if (_userservce.ExistsUserPrivateNumber(userDto))
            {
                throw new AppException("User-ი ამ -'" + userDto.PrivateNumber + "'-ით უკვე არსებობს");
            }
            else if (_userservce.ExistsUserPhone(userDto))
            {
                throw new AppException("User-ი ამ -'" + userDto.Phone + "'-ით უკვე არსებობს");
            }
            else if (!_userservce.ExistsCountryId(userDto))
            {
                throw new AppException("'" + userDto.CountryId + "'-აიდზე არ არის ქვეყნის სახელი");
            }
            else
            {
                return userDto;
            }

        }

        public UserEditDto AppExceptionOrUserEditDto(UserEditDto userEditDto)
        {
            var userDto = new UserDto();
            userDto.Email = userEditDto.Email.Trim();
            userDto.Phone = userEditDto.Phone.Trim();
            userDto.CountryId = userEditDto.CountryId;
            userDto.PrivateNumber = userEditDto.PrivateNumber.Trim();
            var user = GetUserByPrivateNumber(userEditDto.PrivateNumber.Trim());

            if (user.Email!= userEditDto.Email && _userservce.ExistsUserEmail(userDto))
            {
                throw new AppException("User-ი ამ -'" + userEditDto.Email + "'-ით უკვე არსებობს");
            }
            else if (user.Phone!= userEditDto.Phone && _userservce.ExistsUserPhone(userDto))
            {
                throw new AppException("User-ი ამ -'" + userEditDto.Phone + "'-ით უკვე არსებობს");
            }
            else if (!_userservce.ExistsCountryId(userDto))
            {
                throw new AppException("'" + userDto.CountryId + "'-აიდზე არ არის ქვეყნის სახელი");
            }
            else
            {
                return userEditDto;
            }
        }
       
    }
    
}
