
using DataAL.Entities.Models;
using DataAL.Entities.UserDbModel;
using DataAL.Page;
using DataAL.Services;
using UserManagment.shared.HashHelper;

namespace DataAL.Repository
{
    public class Userservce : IUserservce
    {
        private readonly DataContext _context;

        public Userservce(DataContext context)
        {
            _context = context;
        }

        public User GetUserByPrivateNumber(string privatenumber)
        {
            return _context.Users.First(x => x.PrivateNumber == privatenumber);
        }

        public bool AddUser(UserDto userDto)
        {
           var Country = GetCountryById(userDto);
           var passwordHash= CreatePasswordHash(userDto.Password);
           var User = new User();

            User.FirstName = userDto.FirstName.Trim();
            User.LastName = userDto.LastName.Trim();
            User.Address = userDto.Address.Trim();
            User.Email = userDto.Email.Trim();
            User.Gender = (Gender)Enum.Parse(typeof(Gender), userDto.GenderType);
            User.Role = (Role)Enum.Parse(typeof(Role), userDto.RoleType);
            User.BirthDate = userDto.BirthDate;
            User.Phone = userDto.Phone.Trim();
            User.Country = Country;
            User.PasswordHash = passwordHash;
            User.PrivateNumber = userDto.PrivateNumber.Trim();
            _context.Users.Add(User);

            return Save();
        }


        public bool EditUser(string privatenumber, UserEditDto userEditDto)
        {
           var User = GetUserByPrivateNumber(userEditDto.PrivateNumber.Trim());
             var Country = GetCountryByUserEditDtoId(userEditDto);

            if (!string.IsNullOrEmpty(userEditDto.FirstName))
                     User.FirstName = userEditDto.FirstName.Trim();

            if (!string.IsNullOrEmpty(userEditDto.LastName))
                User.LastName = userEditDto.LastName.Trim();

            if (!string.IsNullOrEmpty(userEditDto.Address))
                User.Address = userEditDto.Address.Trim();

            if (!string.IsNullOrEmpty(userEditDto.Email))
                User.Email = userEditDto.Email.Trim();

            if (!string.IsNullOrEmpty(userEditDto.GenderType))
                User.Gender = (Gender)Enum.Parse(typeof(Gender), userEditDto.GenderType);

            if (!string.IsNullOrEmpty(userEditDto.RoleType))
                User.Role = (Role)Enum.Parse(typeof(Role), userEditDto.RoleType);

            if(User.BirthDate!= userEditDto.BirthDate)
                User.BirthDate = userEditDto.BirthDate;

            if (!string.IsNullOrEmpty(userEditDto.Phone))
                User.Phone = userEditDto.Phone.Trim();

                User.Country = Country;

            if (!string.IsNullOrEmpty(userEditDto.Password))
            {
                var passwordHash = CreatePasswordHash(userEditDto.Password.Trim());
                User.PasswordHash = passwordHash;
            }     
            _context.Users.Update(User);

            return Save();
        }

        public string CreatePasswordHash(string password)
        {
            return HashHelper.Sha512Hex(password.Trim());
        }

        public bool ExistsCountryId(UserDto userDto)
        {
            return _context.Countries.Any(x => x.Id == userDto.CountryId);
        }

        public bool ExistsUserEmail(UserDto userDto)
        {
            return _context.Users.Any(x => x.Email == userDto.Email.Trim());
        }


        public bool ExistsUserPhone(UserDto userDto)
        {
            return _context.Users.Any(x => x.Phone == userDto.Phone.Trim());
        }

        public bool ExistsUserPrivateNumber(UserDto userDto)
        {
            return _context.Users.Any(x => x.PrivateNumber == userDto.PrivateNumber.Trim());
        }


        public Country GetCountryById(UserDto userDto)
        {
            return _context.Countries.First(c => c.Id == userDto.CountryId);
        }

        public UsersGetDto UsersGetDtoByPrivateNumber(string privatenumber)
        {
           
            return  ListOfUsers().First(x=>x.PrivateNumber== privatenumber);
        }

        public PagedList<UsersGetDto> GetUsers(UserParameters userParameters)
        {      
            return PagedList<UsersGetDto>.ToPagedList(ListOfUsers(),userParameters.PageNamber,userParameters.PageSize);

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UserExists(string privatenumber)
        {
            return _context.Users.Any(p => p.PrivateNumber.Trim()==privatenumber.Trim());
        }


        public IQueryable<UsersGetDto>  ListOfUsers()
        {
            var ListOfUsers = (from U in _context.Users
                     join C in _context.Countries
                     on U.Country.Id equals C.Id
                    where U.Active == 1
                     select new
                     {
                         privateNumber = U.PrivateNumber,
                         firstName = U.FirstName,
                         lastName = U.LastName,
                         phone = U.Phone,
                         email = U.Email,
                         birthDate = U.BirthDate,
                         address = U.Address,
                         roleType = (Role)U.Role,
                         country = C.Name,
                         genderType = (Gender)U.Gender,
                         countryId=C.Id
                     }).Select(x => new UsersGetDto
                     {
                         PrivateNumber = x.privateNumber,
                         FirstName = x.firstName,
                         LastName = x.lastName,
                         Phone = x.phone,
                         Email = x.email,
                         BirthDate = x.birthDate,
                         Address = x.address,
                         RoleType = x.roleType.ToString(),
                         GenderType = x.genderType.ToString(),
                         Country = x.country,
                         CountryId=x.countryId
                     });

            return ListOfUsers;
        }

        public Country GetCountryByUserEditDtoId(UserEditDto userEditDto)
        {
            return _context.Countries.First(c => c.Id == userEditDto.CountryId);
        }

        public bool DeleteUser(string privatenumber)
        {
            var user = GetUserByPrivateNumber(privatenumber);
            _context.Users.Remove(user);
            return Save();
        }

        public bool PassingTheUser(string privatenumber)
        {
            var user = GetUserByPrivateNumber(privatenumber);
            user.Active = 0;

            _context.Users.Update(user);

            return Save();

        }

        public bool ActivationOfUser(string privatenumber)
        {
            var user = GetUserByPrivateNumber(privatenumber);
            user.Active = 1;

            _context.Users.Update(user);

            return Save();
        }
    }
}
