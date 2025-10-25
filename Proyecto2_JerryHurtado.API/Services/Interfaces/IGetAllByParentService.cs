namespace Proyecto2_JerryHurtado.API.Services.Interfaces
{
    public interface IGetAllByParentService<T>
    {
        T? GetById(int id);
        List<T> GetAllById(int parentId);
    }
}