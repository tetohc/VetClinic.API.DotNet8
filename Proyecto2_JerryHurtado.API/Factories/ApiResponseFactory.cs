using Proyecto2_JerryHurtado.API.Models.Responses;

namespace Proyecto2_JerryHurtado.API.Factories
{
    /// <summary>
    /// Proporciona métodos para construir respuestas estándar de éxito o fallo en la API.
    /// </summary>
    public static class ApiResponseFactory
    {
        /// <summary>
        /// Crea una respuesta exitosa con datos y mensaje.
        /// </summary>
        /// <param name="statusCode">Código de estado HTTP (por defecto 200).</param>
        /// <param name="data">Datos devueltos por la operación.</param>
        /// <param name="message">Mensaje descriptivo.</param>
        /// <returns>Una instancia de respuesta con éxito.</returns>
        public static ApiResponse<T> Success<T>(int statusCode = StatusCodes.Status200OK, T? data = default, string message = "")
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Success = true,
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// Crea una respuesta fallida con mensaje y código de error.
        /// </summary>
        /// <param name="statusCode">Código de estado HTTP (por defecto 400).</param>
        /// <param name="data">Datos devueltos por la operación.</param>
        /// <param name="message">Mensaje descriptivo del error.</param>
        /// <returns>Una instancia de respuesta con fallo.</returns>
        public static ApiResponse<T> Failure<T>(int statusCode = StatusCodes.Status400BadRequest, T? data = default, string message = "")
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Success = false,
                Message = message,
                Data = data
            };
        }
    }
}