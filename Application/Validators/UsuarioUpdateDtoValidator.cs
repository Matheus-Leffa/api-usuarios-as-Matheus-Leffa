using FluentValidation;
using Application.DTOs;
using Application.Interfaces;

public class UsuarioUpdateDtoValidator : AbstractValidator<UsuarioUpdateDto>
{
    public UsuarioUpdateDtoValidator(IUsuarioRepository repository)
    {
        RuleFor(user => user.Nome)
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage("O nome deve ter no mínimo 3 caracteres.")
            .MaximumLength(100)
            .WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(async (dto, email, context, ct) =>
            {
                var idDaRota = (int)context.RootContextData["Id"];

                var existente = await repository.GetByEmailAsync(email, ct);

                if (existente == null) return true;

                return existente.Id == idDaRota;
            })
            .WithMessage("Email já cadastrado.");

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
            .When(user => !string.IsNullOrWhiteSpace(user.Telefone));
    }
}