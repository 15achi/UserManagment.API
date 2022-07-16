using DataAL.Entities.Models;
using DataAL.Entities.UserDbModel;
using FluentValidation;

namespace UserManagment.API.validator
{
    public class UserValidator : AbstractValidator<UserEditDto>
    {
        public UserValidator()
        {
            RuleFor(u => u.PrivateNumber).NotNull().NotEmpty().Length(11)
                 .Must(IsValidNamber)
                 .WithMessage("{PrivateNumber} არ უნდა იყოს  ცარიელი/ნალი, უნდა იყოს 11 ციფრი");

            RuleFor(u => u.FirstName);
            // .Length(2, 25).When(x => !string.IsNullOrEmpty(x.FirstName))
            // .WithMessage("{FirstName} სახელი უნდა იყოს მინ 2 და მაქს 25 სიმბოლო")
            // .Must(IsValidName).When(x => !string.IsNullOrEmpty(x.FirstName))
            // .WithMessage("{FirstName} უნდა შედგებოდეს ასობგერებისგან");


            RuleFor(u => u.LastName).NotNull().NotEmpty();
              //  .Length(2, 25)
             //   .Length(2, 25).When(x => !string.IsNullOrEmpty(x.LastName))
             //   .WithMessage("{LastName} სახელი უნდა იყოს მინ 2 და მაქს 25 სიმბოლო")
             //   .Must(IsValidName).When(x => !string.IsNullOrEmpty(x.LastName))
            //    .WithMessage("{LastName} უნდა შედგებოდეს ასობგერებისგან");

            When(x => !string.IsNullOrEmpty(x.Password), () =>
            {
                RuleFor(u => u.Password).Password();
            });

            RuleFor(u => u.Phone)
                .Length(1, 50).When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("{Phone}-ი უნდა შედგებოდეს  ციფრისგან ");

            RuleFor(u => u.BirthDate).NotNull().NotEmpty().WithMessage("{BirthDate} არ უნდა იყოს  ცარიელი/ნალი");
               // .GreaterThan(p => new DateTime(1922, 1, 1)).When(x => !x.Equals(null))
               // .LessThan(p => new DateTime(2012, 1, 1)).When(x => !x.Equals(null)).WithMessage("{BirthDate} უნდა იყოს მეტი 1922 და ნაკლები 2012");

            RuleFor(u => u.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("{Email} უნდა იყოს მეილი");

            RuleFor(u => u.Address);

            RuleFor(u => u.GenderType)
                .IsEnumName(typeof(Gender)).When(x => !string.IsNullOrEmpty(x.GenderType))
                .WithMessage("უნდა გადაეცეს  {Male} , {Female} ან {Others}");

            RuleFor(u => u.RoleType)
                .IsEnumName(typeof(Role)).When(x => !string.IsNullOrEmpty(x.RoleType))
                .WithMessage("უნდა გადაეცეს  {User} ან {Admin}");

            RuleFor(u => u.CountryId)
                .GreaterThanOrEqualTo(0).WithMessage("უნდა გადაეცეს CountryId ");

        }

        private object Must(Func<string, bool> isValidNamber)
        {
            throw new NotImplementedException();
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
