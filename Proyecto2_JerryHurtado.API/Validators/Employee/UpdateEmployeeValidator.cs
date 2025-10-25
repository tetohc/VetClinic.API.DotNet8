using FluentValidation;
using Proyecto2_JerryHurtado.API.Models.Dtos.Employee;

namespace Proyecto2_JerryHurtado.API.Validators.Employee
{
    public class UpdateEmployeeValidator : AbstractValidator<EmployeeUpdateDto>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El identificador del empleado es obligatorio.");

            RuleFor(x => x.PersonalIdNumber)
                .NotEmpty().WithMessage("El número de identificación es obligatorio.")
                .Matches(@"^\d-\d{4}-\d{4}$")
                .WithMessage("Formato inválido: se espera X-XXXX-XXXX.");

            RuleFor(x => x.Birthdate)
                .LessThan(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("La fecha de nacimiento debe ser anterior a hoy.");

            RuleFor(x => x.HireDate)
                .GreaterThan(x => x.Birthdate)
                .WithMessage("La fecha de contratación debe ser posterior a la fecha de nacimiento.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("La fecha de contratación no puede ser futura.");

            RuleFor(x => x.DailySalary)
                .GreaterThan(0).WithMessage("El salario diario debe ser mayor a cero.");

            RuleFor(x => x.TerminationDate)
                .GreaterThan(x => x.HireDate)
                .When(x => x.TerminationDate != default)
                .WithMessage("La fecha de salida debe ser posterior a la fecha de contratación.");

            RuleFor(x => x.Type)
                .InclusiveBetween(1, 5)
                .WithMessage("El tipo de empleado debe estar entre 1 y 5.");
        }
    }
}