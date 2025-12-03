using Application.DTOs;
using Application.Interfaces;
using FluentValidation;

namespace Application.Validators
{
    public class UsuarioCreateDtoValidator : AbstractValidator<UsuarioCreateDto>
    {
        public UsuarioCreateDtoValidator(IUsuarioRepository repository)
        {
            RuleFor(user => user.Nome)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);

            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (email, ct) =>
                {
                    var VerificarEmail = await repository.EmailExistsAsync(email.ToLower(), ct);
                    return !VerificarEmail;
                })
                .WithMessage("Este Email já está cadastrado.");

            RuleFor(user => user.Senha)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(user => user.DataNascimento)
                .NotEmpty()
                .Must(data =>
                {
                    int idade = DateTime.Today.Year - data.Year;
                    if (data.Date > DateTime.Today.AddYears(-idade))
                        idade--;
                    return idade >= 18;
                })
                .WithMessage("Usuário deve ser maior de idade.");

            RuleFor(user => user.Telefone)
                .Matches(@"^(\+55\s?)?(\(?\d{2}\)?\s?)?9?\d{4}-?\s?\d{4}$")
                .WithMessage("Utilize o formato correto. Exemplo: (00) 12345-6789")
                .When(user => !string.IsNullOrWhiteSpace(user.Telefone))
                .WithMessage("Telefone inválido.");
        }
    }
}