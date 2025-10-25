using FluentValidation;
using Proyecto2_JerryHurtado.API.Models.Dtos.Customer;

/// <summary>
/// Valida las reglas de negocio para crear un nuevo cliente.
/// </summary>
public class CreateCustomerValidator : AbstractValidator<CustomerCreateDto>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre completo es obligatorio.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
            .EmailAddress().WithMessage("Debe ingresar un correo electrónico válido.");

        RuleFor(x => x.PersonalIdNumber)
            .NotEmpty().WithMessage("El número de identificación es obligatorio.")
            .Matches(@"^\d-\d{4}-\d{4}$")
            .WithMessage("Formato inválido: se espera X-XXXX-XXXX.");

        RuleFor(x => x.ProvinceId)
            .GreaterThan(0).WithMessage("Debe seleccionar una provincia válida.");

        RuleFor(x => x.CantonId)
            .GreaterThan(0).WithMessage("Debe seleccionar un cantón válido.");

        RuleFor(x => x.DistrictId)
            .GreaterThan(0).WithMessage("Debe seleccionar un distrito válido.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("La dirección es obligatoria.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("El número de teléfono es obligatorio.")
            .Matches(@"^\d{4}-\d{4}$")
            .WithMessage("Formato inválido: se espera XXXX-XXXX.");

        RuleFor(x => x.ContactPreference)
            .InclusiveBetween(1, 2).WithMessage("Debe seleccionar una preferencia de contacto válida.");
    }
}