namespace Proyecto2_JerryHurtado.API.Services.Interfaces
{
    /// <summary>
    /// Interfaz para eliminar procedimientos asociados a clientes o mascotas.
    /// </summary>
    public interface IProcedureCascadeDeletionService
    {
        /// <summary>
        /// Elimina todos los procedimientos asociados al cliente especificado.
        /// </summary>
        /// <param name="customerId">ID del cliente.</param>
        void DeleteProceduresByCustomerId(Guid customerId);

        /// <summary>
        /// Elimina todos los procedimientos asociados a la mascota especificada.
        /// </summary>
        /// <param name="petId">ID de la mascota.</param>
        void DeleteProceduresByPetId(Guid petId);
    }
}