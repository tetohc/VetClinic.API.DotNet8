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
        Task<bool> Create(TCreateDto createDto);

        Task<bool> Update(TUpdateDto updateDto);

        Task<bool> Delete(Guid id);

        Task<TReadDto?> GetById(Guid id);

        Task<List<TReadDto>> GetAll();

        Task<List<TReadDto>> Search(string query);

        Task<int> Count();
    }
}