using DataAL.Entities.Models;
using DataAL.Entities.UserDbModel;
using FluentValidation;

namespace UsersManagment.Models
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(u => u.PrivateNumber).NotNull().NotEmpty().Length(11)
                 .Must(IsValidNamber)
                 .WithMessage("{PrivateNumber} არ უნდა იყოს  ცარიელი/ნალი, უნდა იყოს 11 ციფრი");

            RuleFor(u => u.FirstName).NotNull().NotEmpty();
            //  .Length(2,25)
            //   .WithMessage("{FirstName} არ უნდა იყოს  ცარიელი/ნალი,სახელი უნდა იყოს მინ 2 და მაქს 25 სიმბოლო")
            //  .Must(IsValidName).WithMessage("{FirstName} უნდა შედგებოდეს ასობგერებისგან") ;

            RuleFor(u => u.LastName).NotNull().NotEmpty();
                //.Length(2, 25)
               // .WithMessage("{LastName} არ უნდა იყოს  ცარიელი/ნალი,გვარი უნდა იყოს მინ 2 და მაქს 25 სიმბოლო")
              //  .Must(IsValidName).WithMessage("{FirstName} უნდა შედგებოდეს ასობგერებისგან");

            RuleFor(u => u.Password).Password();

          //  RuleFor(u => u.ConfirmPassword).Password();

         //   RuleFor(u => u.Password).Equal(u => u.ConfirmPassword)
          //      .WithMessage(" {Password}-ი უნდა იყოს ტოლი {ConfirmPassword}-ის");

            RuleFor(u => u.Phone).NotNull().NotEmpty()
                .WithMessage("{Phone} არ უნდა იყოს  ცარიელი/ნალი")
                .MaximumLength(50)
                .Must(IsValidNamber).WithMessage("{Phone} უნდა შედგებოდეს ციფრებისგან");

            RuleFor(u => u.BirthDate).NotNull().NotEmpty()
                .WithMessage("{BirthDate} არ უნდა იყოს  ცარიელი/ნალი")
                .GreaterThan(p => new DateTime(1922, 1, 1))
                .LessThan(p => new DateTime(2012, 1, 1)).WithMessage("{BirthDate} უნდა იყოს მეტი 1922 და ნაკლები 2012");

            RuleFor(u => u.Email).NotNull().NotEmpty().EmailAddress()
                .WithMessage("{Email} არ უნდა იყოს ცარილეი/ნალი და უნდა იყოს მეილი");

            RuleFor(u => u.Address).NotNull().NotEmpty()
                .WithMessage("{Address} არ უნდა იყოს  ცარიელი/ნალი");

            RuleFor(u => u.GenderType).NotNull().NotEmpty()
                .WithMessage("{Gender} არ უნდა იყოს  ცარიელი/ნალი")
                .IsEnumName(typeof(Gender)).WithMessage("უნდა გადაეცეს  {Male} ან {Female}");

            RuleFor(u => u.RoleType).NotNull().NotEmpty()
                .WithMessage("{Role} არ უნდა იყოს  ცარიელი/ნალი")
                .IsEnumName(typeof(Role)).WithMessage("უნდა გადაეცეს  {Admin} ან {User}");

            RuleFor(u => u.CountryId).NotNull().NotEmpty()
                .WithMessage("{CountryId} არ უნდა იყოს  ცარიელი/ნალი");
                
        }

        private bool IsValidName(string name)
        {
            return name.All(char.IsLetter);
        }
      
        private bool IsValidNamber(string Namber)
        {
            return Namber.All(char.IsNumber); 
        }
    }
}
