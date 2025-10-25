using Proyecto2_JerryHurtado.API.Models.Dtos.PetProcedure;
using Proyecto2_JerryHurtado.API.Models.Entities;

namespace Proyecto2_JerryHurtado.API.Mappers
{
    /// <summary>
    /// Esta clase proporciona métodos de extensión para mapear entidades de procedimientos de mascotas a DTOs.
    /// </summary>
    public static class PetProcedureMapper
    {
        /// <summary>
        /// Convierte un <see cref="PetProcedureCreateDto"/> en una entidad <see cref="PetProcedureEntity"/>.
        /// </summary>
        /// <param name="dto">DTO con los datos de entrada.</param>
        /// <returns>Entidad lista para ser procesada o almacenada.</returns>
        public static PetProcedureEntity FromCreateDtoToEntity(this PetProcedureCreateDto dto) =>
            new()
            {
                Id = Guid.NewGuid(),
                CustomerId = dto.CustomerId,
                PetId = dto.PetId,
                ProcedureTypeId = dto.ProcedureTypeId,
                Status = dto.Status
            };

        /// <summary>
        /// Convierte un <see cref="PetProcedureEntity"/> en una entidad <see cref="PetProcedureDto"/>.
        /// </summary>
        /// <param name="entity">Entidad de origen.</param>
        /// <returns>DTO resultante.</returns>
        public static PetProcedureDto ToDto(this PetProcedureEntity entity) =>
            new()
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                PetId = entity.PetId,
                ProcedureTypeId = entity.ProcedureTypeId,
                Status = entity.Status,
                Customer = entity.Customer?.ToDto() ?? new(),
                Pet = entity.Pet?.ToSimpleDto() ?? new()
            };

        /// <summary>
        /// Convierte una lista de <see cref="PetProcedureEntity"/> en una lista de <see cref="PetProcedureDto"/>.
        /// </summary>
        /// <param name="entities">Lista de origen.</param>
        /// <returns>Lista de DTOs resultante.</returns>
        public static List<PetProcedureDto> ToDtosList(this IEnumerable<PetProcedureEntity> entities) =>
           entities.Select(x => ToDto(x)).ToList();
    }
}