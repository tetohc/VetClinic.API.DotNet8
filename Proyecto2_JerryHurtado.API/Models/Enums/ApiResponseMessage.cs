using System.ComponentModel.DataAnnotations;

namespace Proyecto2_JerryHurtado.API.Models.Enums
{
    /// <summary>
    /// Define mensajes estándar para respuestas de la API, alineados con códigos HTTP.
    /// </summary>
    public enum ApiResponseMessage
    {
        [Display(Name = "Solicitud exitosa.")]
        Ok200 = 1,

        [Display(Name = "Registro creado exitosamente.")]
        Created201,

        [Display(Name = "Error al crear el registro: solicitud inválida.")]
        BadRequestCreate400,

        [Display(Name = "Registro actualizado exitosamente.")]
        Updated200,

        [Display(Name = "Error al actualizar el registro: solicitud inválida.")]
        BadRequestUpdate400,

        [Display(Name = "Registro eliminado exitosamente.")]
        Deleted200,

        [Display(Name = "Error al eliminar el registro: solicitud inválida.")]
        BadRequestDelete400,

        [Display(Name = "Recurso no encontrado")]
        NotFound404,

        [Display(Name = "Error al obtener el registro: se requiere un identificador válido.")]
        BadRequestGetById400,

        [Display(Name = "Ha ocurrido un error inesperado.")]
        InternalServerError500
    }
}