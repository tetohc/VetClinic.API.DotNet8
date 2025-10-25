using FluentValidation;
using Proyecto2_JerryHurtado.API.Models.Dtos.PetProcedure;

namespace Proyecto2_JerryHurtado.API.Validators.PetProcedure
{
    /// <summary>
    /// Valida las reglas de negocio para crear un nuevo procedimiento de mascota.
    /// </summary>
    public class CreatePetProcedureValidator : AbstractValidator<PetProcedureCreateDto>
    {
        public CreatePetProcedureValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("El identificador del cliente es obligatorio.");

            RuleFor(x => x.PetId)
                .NotEmpty().WithMessage("El identificador de la mascota es obligatorio.");

            RuleFor(x => x.ProcedureTypeId)
                .InclusiveBetween(1, 14).WithMessage("El tipo de procedimiento debe estar entre 1 y 14.");

            RuleFor(x => x.Status)
                .InclusiveBetween(1, 3).WithMessage("El estado del procedimiento debe estar entre 1 y 3.");
        }
    }
}