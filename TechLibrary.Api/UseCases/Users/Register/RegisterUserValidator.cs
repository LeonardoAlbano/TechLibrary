using FluentValidation;
using TechLibrary.Communication.Requests;

namespace TechLibrary.Api.UseCases.Users.Register;

public class RegisterUserValidator : AbstractValidator<RequestUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage("o nome é obrigatorio!");
        RuleFor(request => request.Email).EmailAddress().WithMessage("O email não é valido");
        RuleFor(request => request.Password).NotEmpty().WithMessage("Senha é obrigatorio!");
        When(request => string.IsNullOrEmpty(request.Password) == false, () =>
        {
            RuleFor(request => request.Password.Length).GreaterThanOrEqualTo(6)
                .WithMessage("A senha é deve ter mais que 6 caracteres!");
        });
    }
}