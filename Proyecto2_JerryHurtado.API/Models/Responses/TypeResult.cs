using FluentValidation.Results;

namespace Proyecto2_JerryHurtado.API.Models.Responses
{
    /// <summary>
    /// Representa un resultado que puede contener un modelo válido o errores de validación.
    /// </summary>
    /// <typeparam name="T">Tipo del modelo devuelto en caso de éxito.</typeparam>
    public class TypedResult<T>
    {
        public T? Model { get; set; }
        public List<ValidationFailure>? Errors { get; set; }
    }
}