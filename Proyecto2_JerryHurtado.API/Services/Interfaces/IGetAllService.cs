namespace Proyecto2_JerryHurtado.API.Services.Interfaces
{
    public interface IGetAllService<T>
    {
        Task<List<T>> GetAll();
    }
}