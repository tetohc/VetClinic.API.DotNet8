using Proyecto2_JerryHurtado.API.Models.Dtos.Customer;

namespace Proyecto2_JerryHurtado.API.Services.Interfaces
{
    /// <summary>
    /// Interfaz para consultar datos de clientes sin modificar el estado del sistema.
    /// </summary>
    public interface IReadOnlyCustomerService
    {
        /// <summary>
        /// Obtiene los datos de un cliente según su identificador único.
        /// </summary>
        /// <param name="id">ID del cliente a consultar.</param>
        /// <returns>DTO con los datos del cliente si existe.</returns>
        CustomerDto? GetById(Guid id);

        /// <summary>
        /// Obtiene una lista de todos los clientes registrados.
        /// </summary>
        /// <returns>Lista de DTOs con los datos de los clientes registrados.</returns>
        List<CustomerDto> GetAll();
    }
}