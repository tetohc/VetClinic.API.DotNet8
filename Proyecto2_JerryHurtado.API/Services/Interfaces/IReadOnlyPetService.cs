using Proyecto2_JerryHurtado.API.Models.Dtos.Pet;

namespace Proyecto2_JerryHurtado.API.Services.Interfaces
{
    /// <summary>
    /// Interfaz para consultar datos de mascotas sin modificar el estado del sistema.
    /// </summary>
    public interface IReadOnlyPetService
    {
        /// <summary>
        /// Obtiene los datos de una mascota según su identificador único.
        /// </summary>
        /// <param name="id">ID de la mascota a consultar.</param>
        /// <returns>DTO con los datos de la mascota si existe.</returns>
        PetDto? GetById(Guid id);

        /// <summary>
        /// Obtiene una lista de todas las mascotas registradas.
        /// </summary>
        /// <returns>Lista de DTOs con los datos de las mascotas registradas.</returns>
        List<PetDto> GetAll();
    }
}