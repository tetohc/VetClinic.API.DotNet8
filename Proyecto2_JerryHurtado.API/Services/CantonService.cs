using Microsoft.EntityFrameworkCore;
using Proyecto2_JerryHurtado.API.Database;
using Proyecto2_JerryHurtado.API.Models.Dtos.Canton;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Services
{
    public class CantonService : IGetAllByParentService<CantonDto>
    {
        private readonly DatabaseContext _databaseContext;

        public CantonService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<CantonDto>> GetAllById(int parentId) =>
            await _databaseContext.Canton.Where(x => x.ProvinceId == parentId)
                .Select(x => new CantonDto { Id = x.Id, Name = x.Name })
                .ToListAsync();
    }
}