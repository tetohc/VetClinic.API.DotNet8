using FluentValidation;
using Proyecto2_JerryHurtado.API.Models.Dtos.Pet;

namespace Proyecto2_JerryHurtado.API.Validators.Pet
{
    /// <summary>
    /// Valida las reglas de negocio para crear una nueva mascota.
    /// </summary>
    public class CreatePetValidator : AbstractValidator<PetCreateDto>
    {
        public CreatePetValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("El identificador del cliente es obligatorio.");

            RuleFor(x => x.PetSpecies)
                .InclusiveBetween(1, 11).WithMessage("La especie de la mascota debe estar entre 1 y 11.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.");

            RuleFor(x => x.Race)
                .NotEmpty().WithMessage("La raza es obligatoria.")
                .MaximumLength(50).WithMessage("La raza no debe exceder los 50 caracteres.");

            RuleFor(x => x.Age)
                .GreaterThanOrEqualTo(0).WithMessage("La edad debe ser un número positivo.");

            RuleFor(x => x.Color)
                .NotEmpty().WithMessage("El color es obligatorio.")
                .MaximumLength(30).WithMessage("El color no debe exceder los 30 caracteres.");

            RuleFor(x => x.LastVisitDate)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("La fecha de última visita no puede ser en el futuro.");
        }
    }
}