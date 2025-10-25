namespace Proyecto2_JerryHurtado.API.Services.Interfaces
{
    /// <summary>
    /// Interfaz para eliminar mascotas asociadas a un cliente.
    /// </summary>
    public interface IPetCascadeDeletionService
    {
        /// <summary>
        /// Elimina todas las mascotas asociadas al cliente especificado.
        /// </summary>
        /// <param name="customerId">ID del cliente.</param>
        void DeletePetsByCustomerId(Guid customerId);
    }
}