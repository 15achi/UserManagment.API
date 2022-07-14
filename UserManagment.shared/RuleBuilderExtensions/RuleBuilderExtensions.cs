using FluentValidation;

public static class RuleBuilderExtensions
{
    public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        var options = ruleBuilder
                      .NotEmpty().WithMessage("{Password} არ უნდა იყოს  ცარიელი")
                      .MinimumLength(6).WithMessage("{Password} არ უნდა იყოს  6 სიმბოლოზე ნაკლები");
                      //.Matches("[A-Z]").WithMessage("{Password} უნდა იყოს დიდი ასო")
                      //.Matches("[a-z]").WithMessage("{Password} უნდა იყოს პატარა ასო")
                      //.Matches("[0-9]").WithMessage("{Password} უნდა იყოს ციფრი")
                      //.Matches("[^a-zA-Z0-9]").WithMessage("{Password} უნდა იყოს სიმბოლო");
        return options;
    }
}
