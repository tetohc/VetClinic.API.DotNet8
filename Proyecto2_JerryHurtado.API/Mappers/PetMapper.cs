using Proyecto2_JerryHurtado.API.Models.Dtos.Pet;
using Proyecto2_JerryHurtado.API.Models.Entities;

namespace Proyecto2_JerryHurtado.API.Mappers
{
    /// <summary>
    /// Esta clase proporciona métodos de extensión para mapear entidades de mascotas a DTOs.
    /// </summary>
    public static class PetMapper
    {
        /// <summary>
        /// Convierte un <see cref="PetCreateDto"/> en una entidad <see cref="PetEntity"/>.
        /// </summary>
        /// <param name="dto">DTO con los datos de entrada.</param>
        /// <returns>Entidad lista para ser procesada o almacenada.</returns>
        public static PetEntity FromCreateDtoToEntity(this PetCreateDto dto) =>
            new()
            {
                Id = Guid.NewGuid(),
                CustomerId = dto.CustomerId,
                PetSpecies = dto.PetSpecies,
                Name = dto.Name.Trim(),
                Race = dto.Race.Trim(),
                Age = dto.Age,
                Color = dto.Color.Trim(),
                LastVisitDate = dto.LastVisitDate
            };

        /// <summary>
        /// Convierte un <see cref="PetDto"/> en una entidad <see cref="PetEntity"/>.
        /// </summary>
        /// <param name="dto">DTO con los datos de entrada.</param>
        /// <returns>Entidad lista para ser procesada o almacenada.</returns>
        public static PetEntity ToEntity(this PetDto dto) =>
            new()
            {
                Id = dto.Id,
                CustomerId = dto.CustomerId,
                PetSpecies = dto.PetSpecies,
                Name = dto.Name.Trim(),
                Race = dto.Race.Trim(),
                Age = dto.Age,
                Color = dto.Color.Trim(),
                LastVisitDate = dto.LastVisitDate
            };

        /// <summary>
        /// Convierte un <see cref="PetEntity"/> en una entidad <see cref="PetDto"/>.
        /// </summary>
        /// <param name="entity">Entidad de origen.</param>
        /// <returns>DTO resultante.</returns>
        public static PetDto ToDto(this PetEntity entity) =>
            new()
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                PetSpecies = entity.PetSpecies,
                Name = entity.Name.Trim(),
                Race = entity.Race.Trim(),
                Age = entity.Age,
                Color = entity.Color.Trim(),
                LastVisitDate = entity.LastVisitDate,
                Owner = entity.Customer?.ToDto() ?? new()
            };

        /// <summary>
        /// Convierte un <see cref="PetEntity"/> en una entidad <see cref="PetSimpleDto"/>.
        /// </summary>
        /// <param name="entity">Entidad de origen.</param>
        /// <returns>DTO resultante.</returns>
        public static PetSimpleDto ToSimpleDto(this PetEntity entity) =>
        new()
        {
            Id = entity.Id,
            CustomerId = entity.CustomerId,
            PetSpecies = entity.PetSpecies,
            Name = entity.Name.Trim(),
            Race = entity.Race.Trim(),
            Age = entity.Age,
            Color = entity.Color.Trim(),
            LastVisitDate = entity.LastVisitDate,
        };

        /// <summary>
        /// Convierte una lista de <see cref="PetEntity"/> en una lista de <see cref="PetDto"/>.
        /// </summary>
        /// <param name="entities">Lista de origen.</param>
        /// <returns>Lista de DTOs resultante.</returns>
        public static List<PetDto> ToDtosList(this IEnumerable<PetEntity> entities) =>
           entities.Select(x => ToDto(x)).ToList();
    }
}