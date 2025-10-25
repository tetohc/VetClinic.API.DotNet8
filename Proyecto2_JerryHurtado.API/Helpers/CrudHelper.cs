using FluentValidation;
using Proyecto2_JerryHurtado.API.Factories;
using Proyecto2_JerryHurtado.API.Models.Enums;
using Proyecto2_JerryHurtado.API.Models.Responses;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Helpers
{
    /// <summary>
    /// Métodos genéricos para operaciones CRUD en controladores.
    /// </summary>
    public static class CrudHelper
    {
        #region Operaciones de Escritura (CUD)

        /// <summary>
        /// Crea un nuevo elemento utilizando el servicio.
        /// </summary>
        /// <typeparam name="TCreateDto">DTO de creación (requerido).</typeparam>
        /// <typeparam name="TUpdateDto">DTO de actualización (inferido desde el servicio).</typeparam>
        /// <typeparam name="TReadDto">DTO de lectura (inferido desde el servicio).</typeparam>
        /// <param name="service">Servicio que gestiona los datos.</param>
        /// <param name="model">Datos del nuevo elemento.</param>
        /// <param name="validator">Validador de reglas para el DTO de creación.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        public static ApiResponse<TypedResult<TCreateDto>> Create<TCreateDto, TUpdateDto, TReadDto>(
            IService<TCreateDto, TUpdateDto, TReadDto> service, TCreateDto model,
            IValidator<TCreateDto> validator)
        {
            try
            {
                var validate = validator.Validate(model);
                if (!validate.IsValid)
                    return ApiResponseFactory.Failure(
                            StatusCodes.Status400BadRequest,
                            new TypedResult<TCreateDto> { Errors = validate.Errors },
                            ApiResponseMessage.BadRequestCreate400.GetDisplayName()
                        );

                bool created = service.Create(model);
                return created
                    ? ApiResponseFactory.Success(
                        StatusCodes.Status201Created,
                        new TypedResult<TCreateDto> { Model = model },
                        ApiResponseMessage.Created201.GetDisplayName()
                    )
                    : ApiResponseFactory.Failure<TypedResult<TCreateDto>>(
                        StatusCodes.Status400BadRequest,
                        data: null,
                        ApiResponseMessage.BadRequestCreate400.GetDisplayName()
                    );
            }
            catch
            {
                return ApiResponseFactory.Failure<TypedResult<TCreateDto>>(
                    StatusCodes.Status500InternalServerError,
                    data: null,
                    ApiResponseMessage.InternalServerError500.GetDisplayName()
                );
            }
        }

        /// <summary>
        /// Actualiza un elemento existente.
        /// </summary>
        /// <typeparam name="TCreateDto">DTO de creación.</typeparam>
        /// <typeparam name="TUpdateDto">DTO de actualización.</typeparam>
        /// <typeparam name="TReadDto">DTO de lectura.</typeparam>
        /// <param name="service">Servicio que gestiona los datos.</param>
        /// <param name="model">Datos actualizados.</param>
        /// <param name="id">ID del elemento.</param>
        /// <param name="validator">Validador de reglas para el DTO de actualización.</param>
        /// <returns>Respuesta HTTP con el resultado.</returns>
        public static ApiResponse<TypedResult<TUpdateDto>> Update<TCreateDto, TUpdateDto, TReadDto>(
            IService<TCreateDto, TUpdateDto, TReadDto> service,
            TUpdateDto model,
            Guid id,
            IValidator<TUpdateDto> validator)
        {
            try
            {
                var isExist = service.GetById(id);
                if (isExist is null)
                    return ApiResponseFactory.Failure<TypedResult<TUpdateDto>>(
                        StatusCodes.Status404NotFound,
                        data: null,
                        ApiResponseMessage.NotFound404.GetDisplayName());

                var validate = validator.Validate(model);
                if (!validate.IsValid)
                    return ApiResponseFactory.Failure(
                            StatusCodes.Status400BadRequest,
                            new TypedResult<TUpdateDto> { Errors = validate.Errors },
                            ApiResponseMessage.BadRequestUpdate400.GetDisplayName()
                        );

                bool updated = service.Update(model);
                return updated
                    ? ApiResponseFactory.Success(
                        StatusCodes.Status200OK,
                        new TypedResult<TUpdateDto> { Model = model },
                        ApiResponseMessage.Updated200.GetDisplayName()
                    )
                    : ApiResponseFactory.Failure<TypedResult<TUpdateDto>>(
                        StatusCodes.Status400BadRequest,
                        data: null,
                        ApiResponseMessage.BadRequestUpdate400.GetDisplayName()
                    );
            }
            catch
            {
                return ApiResponseFactory.Failure<TypedResult<TUpdateDto>>(
                    StatusCodes.Status500InternalServerError,
                    data: null,
                    ApiResponseMessage.InternalServerError500.GetDisplayName()
                );
            }
        }

        /// <summary>
        /// Elimina un elemento por ID.
        /// </summary>
        /// <typeparam name="TCreateDto">DTO de creación.</typeparam>
        /// <typeparam name="TUpdateDto">DTO de actualización.</typeparam>
        /// <typeparam name="TReadDto">DTO de lectura.</typeparam>
        /// <param name="service">Servicio que gestiona los datos.</param>
        /// <param name="id">ID del elemento.</param>
        /// <returns>Respuesta HTTP con el resultado.</returns>
        public static ApiResponse<TReadDto> Delete<TCreateDto, TUpdateDto, TReadDto>(
            IService<TCreateDto, TUpdateDto, TReadDto> service, Guid id)
        {
            try
            {
                var isExist = service.GetById(id);
                if (isExist is null)
                    return ApiResponseFactory.Failure<TReadDto>(
                        statusCode: StatusCodes.Status404NotFound,
                        message: ApiResponseMessage.NotFound404.GetDisplayName());

                bool deleted = service.Delete(id);
                return deleted
                    ? ApiResponseFactory.Success(
                        statusCode: StatusCodes.Status200OK,
                        data: isExist,
                        message: ApiResponseMessage.Deleted200.GetDisplayName()
                    )
                    : ApiResponseFactory.Failure<TReadDto>(
                        statusCode: StatusCodes.Status400BadRequest,
                        message: ApiResponseMessage.BadRequestDelete400.GetDisplayName()
                    );
            }
            catch
            {
                return ApiResponseFactory.Failure<TReadDto>(
                    statusCode: StatusCodes.Status500InternalServerError,
                    message: ApiResponseMessage.InternalServerError500.GetDisplayName()
                );
            }
        }

        #endregion Operaciones de Escritura (CUD)

        #region Operaciones de Lectura (R)

        /// <summary>
        /// Obtiene un elemento por su identificador.
        /// </summary>
        /// <typeparam name="TCreateDto">DTO de creación (inferido desde el servicio).</typeparam>
        /// <typeparam name="TUpdateDto">DTO de actualización (inferido desde el servicio).</typeparam>
        /// <typeparam name="TReadDto">DTO de lectura (requerido).</typeparam>
        /// <param name="service">Servicio que gestiona los datos.</param>
        /// <param name="id">Identificador único del elemento.</param>
        /// <returns>Respuesta HTTP con el elemento encontrado o mensaje de error.</returns>
        public static ApiResponse<TReadDto> GetById<TCreateDto, TUpdateDto, TReadDto>(
            IService<TCreateDto, TUpdateDto, TReadDto> service, Guid id)
        {
            if (id == Guid.Empty)
            {
                return ApiResponseFactory.Failure<TReadDto>(
                    statusCode: StatusCodes.Status400BadRequest,
                    message: ApiResponseMessage.BadRequestGetById400.GetDisplayName()
                );
            }

            var data = service.GetById(id);
            if (data is null)
            {
                return ApiResponseFactory.Failure<TReadDto>(
                    statusCode: StatusCodes.Status404NotFound,
                    message: ApiResponseMessage.NotFound404.GetDisplayName()
                );
            }

            return ApiResponseFactory.Success(
                StatusCodes.Status200OK,
                data,
                ApiResponseMessage.Ok200.GetDisplayName()
            );
        }

        /// <summary>
        /// Obtiene todos los elementos del servicio.
        /// </summary>
        /// <typeparam name="TCreateDto">DTO de creación (inferido desde el servicio).</typeparam>
        /// <typeparam name="TUpdateDto">DTO de actualización (inferido desde el servicio).</typeparam>
        /// <typeparam name="TReadDto">DTO de lectura (requerido).</typeparam>
        /// <param name="service">Servicio que gestiona los datos.</param>
        /// <returns>Respuesta HTTP con la lista de elementos.</returns>
        public static ApiResponse<List<TReadDto>> GetList<TCreateDto, TUpdateDto, TReadDto>(
            IService<TCreateDto, TUpdateDto, TReadDto> service)
        {
            var data = service.GetAll();
            return ApiResponseFactory.Success(
                statusCode: StatusCodes.Status200OK,
                data,
                message: ApiResponseMessage.Ok200.GetDisplayName()
            );
        }

        /// <summary>
        /// Filtra los elementos del servicio según el criterio de búsqueda proporcionado.
        /// </summary>
        /// <typeparam name="TCreateDto">DTO de creación (inferido desde el servicio).</typeparam>
        /// <typeparam name="TUpdateDto">DTO de actualización (inferido desde el servicio).</typeparam>
        /// <typeparam name="TReadDto">DTO de lectura (requerido).</typeparam>
        /// <param name="service">Servicio que gestiona los datos.</param>
        /// <param name="query">Texto de búsqueda utilizado para filtrar los elementos.</param>
        /// <returns>Respuesta HTTP con la lista de elementos filtrados.</returns>
        public static ApiResponse<List<TReadDto>> Search<TCreateDto, TUpdateDto, TReadDto>(
            IService<TCreateDto, TUpdateDto, TReadDto> service, string query)
        {
            var data = service.Search(query.Trim());
            return ApiResponseFactory.Success(
                statusCode: StatusCodes.Status200OK,
                data,
                message: ApiResponseMessage.Ok200.GetDisplayName()
            );
        }

        /// <summary>
        /// Obtiene la cantidad total de elementos gestionados por el servicio.
        /// </summary>
        /// <typeparam name="TCreateDto">DTO de creación (inferido desde el servicio).</typeparam>
        /// <typeparam name="TUpdateDto">DTO de actualización (inferido desde el servicio).</typeparam>
        /// <typeparam name="TReadDto">DTO de lectura (requerido).</typeparam>
        /// <param name="service">Servicio que gestiona los datos.</param>
        /// <returns>Respuesta HTTP con el número total de elementos.</returns>
        public static ApiResponse<int> Count<TCreateDto, TUpdateDto, TReadDto>(
            IService<TCreateDto, TUpdateDto, TReadDto> service)
        {
            var count = service.Count();
            return ApiResponseFactory.Success(
                statusCode: StatusCodes.Status200OK,
                data: count,
                message: ApiResponseMessage.Ok200.GetDisplayName()
            );
        }

        #endregion Operaciones de Lectura (R)
    }
}