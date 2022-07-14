using DataAL;
using DataAL.Entities.Models;
using UserManagment.shared.HashHelper;

namespace UserManagment.API
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.Users.Any())
            {
                var user = new User()
                {
                    PrivateNumber = "12345678910",
                    FirstName = "Admin",
                    LastName = "Admin",
                    PasswordHash = HashHelper.Sha512Hex("1"),
                    Phone = "599112233",
                    Email = "Admin@gmail.com",
                    BirthDate = new DateTime(2000, 1, 1),
                    Address = "ქ.თბილის , რუსთაველის  N 1",
                    Role = Role.Admin,
                    Gender = Gender.Male,
                    Country = new Country()
                    {
                        Name = "India"
                    }

                };

                dataContext.Users.AddRange(user);
                dataContext.SaveChanges();
            }
        }


    }
}
