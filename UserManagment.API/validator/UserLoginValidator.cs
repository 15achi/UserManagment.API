using DataAL.Entities.UserDbModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.BLL.Models.Users
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(u => u.PrivateNumber).NotNull().NotEmpty().Length(11)
                 .Must(IsValidNamber)
                 .WithMessage("{PrivateNumber} არ უნდა იყოს  ცარიელი/ნალი, უნდა იყოს 11 ციფრი");
            RuleFor(u => u.Password).NotNull().NotEmpty()
                .WithMessage("{Password} არ უნდა იყოს  ცარიელი/ნალი");

        }

        private bool IsValidNamber(string Namber)
        {
            return Namber.All(char.IsNumber);
        }
    }
}
