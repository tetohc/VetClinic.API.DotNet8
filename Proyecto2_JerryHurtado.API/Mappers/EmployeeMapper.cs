using Proyecto2_JerryHurtado.API.Models.Dtos.Employee;
using Proyecto2_JerryHurtado.API.Models.Entities;

namespace Proyecto2_JerryHurtado.API.Mappers
{
    /// <summary>
    /// Esta clase proporciona métodos de extensión para mapear entidades de empleados a DTOs.
    /// </summary>
    public static class EmployeeMapper
    {
        /// <summary>
        /// Convierte un <see cref="EmployeeCreateDto"/> en una entidad <see cref="EmployeeEntity"/>.
        /// </summary>
        /// <param name="dto">DTO con los datos de entrada.</param>
        /// <returns>Entidad lista para ser procesada o almacenada.</returns>
        public static EmployeeEntity FromCreateDtoToEntity(this EmployeeCreateDto dto) =>
            new()
            {
                Id = Guid.NewGuid(),
                PersonalIdNumber = dto.PersonalIdNumber.Trim(),
                Birthdate = dto.Birthdate,
                HireDate = dto.HireDate,
                DailySalary = dto.DailySalary,
                TerminationDate = dto.TerminationDate,
                Type = dto.Type
            };

        /// <summary>
        /// Convierte un <see cref="EmployeeEntity"/> en una entidad <see cref="EmployeeDto"/>.
        /// </summary>
        /// <param name="entity">Entidad de origen.</param>
        /// <returns>DTO resultante.</returns>
        public static EmployeeDto ToDto(this EmployeeEntity entity) =>
            new()
            {
                Id = entity.Id,
                PersonalIdNumber = entity.PersonalIdNumber.Trim(),
                Birthdate = entity.Birthdate,
                HireDate = entity.HireDate,
                DailySalary = entity.DailySalary,
                TerminationDate = entity.TerminationDate,
                Type = entity.Type
            };

        /// <summary>
        /// Convierte una lista de <see cref="EmployeeEntity"/> en una lista de <see cref="EmployeeDto"/>.
        /// </summary>
        /// <param name="entities">Lista de origen.</param>
        /// <returns>Lista de DTOs resultante.</returns>
        public static List<EmployeeDto> ToDtosList(this IEnumerable<EmployeeEntity> entities) =>
            entities.Select(x => ToDto(x)).ToList();
    }
}