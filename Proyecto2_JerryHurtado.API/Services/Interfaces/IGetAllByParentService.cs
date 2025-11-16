namespace Proyecto2_JerryHurtado.API.Services.Interfaces
{
    public interface IGetAllByParentService<T>
    {
        Task<List<T>> GetAllById(int parentId);
    }
}