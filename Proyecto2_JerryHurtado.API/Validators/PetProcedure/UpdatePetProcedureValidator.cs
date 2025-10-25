using FluentValidation;
using Proyecto2_JerryHurtado.API.Models.Dtos.PetProcedure;

namespace Proyecto2_JerryHurtado.API.Validators.PetProcedure
{
    /// <summary>
    /// Valida las reglas de negocio para actualizar un procedimiento de mascota existente.
    /// </summary>
    public class UpdatePetProcedureValidator : AbstractValidator<PetProcedureUpdateDto>
    {
        public UpdatePetProcedureValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El identificador del procedimiento es obligatorio.");

            RuleFor(x => x.ProcedureTypeId)
                .InclusiveBetween(1, 14).WithMessage("El tipo de procedimiento debe estar entre 1 y 14.");

            RuleFor(x => x.Status)
                .InclusiveBetween(1, 3).WithMessage("El estado del procedimiento debe estar entre 1 y 3.");
        }
    }
}