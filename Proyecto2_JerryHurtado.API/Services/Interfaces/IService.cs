namespace Proyecto2_JerryHurtado.API.Services.Interfaces
{
    /// <summary>
    /// Interfaz genérica para operaciones CRUD y de búsqueda.
    /// </summary>
    /// <typeparam name="TCreateDto">Tipo de DTO utilizado para crear nuevos registros.</typeparam>
    /// <typeparam name="TUpdateDto">Tipo de DTO utilizado para actualizar registros existentes.</typeparam>
    /// <typeparam name="TReadDto">Tipo de DTO utilizado para devolver datos al cliente.</typeparam>
    public interface IService<TCreateDto, TUpdateDto, TReadDto>
    {
        bool Create(TCreateDto createDto);

        bool Update(TUpdateDto updateDto);

        bool Delete(Guid id);

        TReadDto? GetById(Guid id);

        List<TReadDto> GetAll();

        List<TReadDto> Search(string query);

        int Count();
    }
}