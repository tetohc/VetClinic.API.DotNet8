using Proyecto2_JerryHurtado.API.Models.Dtos.Customer;
using Proyecto2_JerryHurtado.API.Models.Entities;

namespace Proyecto2_JerryHurtado.API.Mappers
{
    /// <summary>
    /// Esta clase proporciona métodos de extensión para mapear entidades de clientes a DTOs.
    /// </summary>
    public static class CustomerMapper
    {
        /// <summary>
        /// Convierte un <see cref="CustomerCreateDto"/> en una entidad <see cref="CustomerEntity"/>.
        /// </summary>
        /// <param name="dto">DTO con los datos de entrada.</param>
        /// <returns>Entidad lista para ser procesada o almacenada.</returns>
        public static CustomerEntity FromCreateDtoToEntity(this CustomerCreateDto dto) =>
            new()
            {
                Id = Guid.NewGuid(),
                PersonalIdNumber = dto.PersonalIdNumber.Trim(),
                Name = dto.Name.Trim(),
                ProvinceId = dto.ProvinceId,
                CantonId = dto.CantonId,
                DistrictId = dto.DistrictId,
                Address = dto.Address.Trim(),
                Email = dto.Email.Trim(),
                PhoneNumber = dto.PhoneNumber.Trim(),
                ContactPreference = dto.ContactPreference
            };

        /// <summary>
        /// Convierte un <see cref="CustomerDto"/> en una entidad <see cref="CustomerEntity"/>.
        /// </summary>
        /// <param name="dto">DTO con los datos de entrada.</param>
        /// <returns>Entidad lista para ser procesada o almacenada.</returns>
        public static CustomerEntity ToEntity(this CustomerDto dto) =>
            new()
            {
                Id = dto.Id,
                PersonalIdNumber = dto.PersonalIdNumber.Trim(),
                Name = dto.Name.Trim(),
                ProvinceId = dto.ProvinceId,
                CantonId = dto.CantonId,
                DistrictId = dto.DistrictId,
                Address = dto.Address.Trim(),
                Email = dto.Email.Trim(),
                PhoneNumber = dto.PhoneNumber.Trim(),
                ContactPreference = dto.ContactPreference
            };

        /// <summary>
        /// Convierte un <see cref="CustomerEntity"/> en una entidad <see cref="CustomerDto"/>.
        /// </summary>
        /// <param name="entity">Entidad de origen.</param>
        /// <returns>DTO resultante.</returns>
        public static CustomerDto ToDto(this CustomerEntity entity) =>
            new()
            {
                Id = entity.Id,
                PersonalIdNumber = entity.PersonalIdNumber?.Trim() ?? string.Empty,
                Name = entity.Name?.Trim() ?? string.Empty,
                ProvinceId = entity.ProvinceId,
                CantonId = entity.CantonId,
                DistrictId = entity.DistrictId,
                Address = entity.Address?.Trim() ?? string.Empty,
                Email = entity.Email?.Trim() ?? string.Empty,
                PhoneNumber = entity.PhoneNumber?.Trim() ?? string.Empty,
                ContactPreference = entity.ContactPreference,
                Province = entity.Province?.Name?.Trim() ?? string.Empty,
                Canton = entity.Canton?.Name?.Trim() ?? string.Empty,
                District = entity.District?.Name?.Trim() ?? string.Empty,
            };

        /// <summary>
        ///  Convierte una lista de <see cref="CustomerEntity"/> en una lista de <see cref="CustomerDto"/>.
        /// </summary>
        /// <param name="entities">Lista de origen.</param>
        /// <returns>Lista de DTOs resultante.</returns>
        public static List<CustomerDto> ToDtosList(this IEnumerable<CustomerEntity> entities) =>
            entities.Select(x => ToDto(x)).ToList();
    }
}