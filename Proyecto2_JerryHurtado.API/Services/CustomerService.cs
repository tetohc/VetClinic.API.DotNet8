using Microsoft.EntityFrameworkCore;
using Proyecto2_JerryHurtado.API.Database;
using Proyecto2_JerryHurtado.API.Mappers;
using Proyecto2_JerryHurtado.API.Models.Dtos.Customer;
using Proyecto2_JerryHurtado.API.Models.Entities;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Services
{
    /// <summary>
    /// Servicio para gestionar operaciones relacionadas con clientes.
    /// </summary>
    public class CustomerService : IService<CustomerCreateDto, CustomerUpdateDto, CustomerDto>
    {
        private readonly DatabaseContext _databaseContext;

        public CustomerService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        #region CRUD

        public async Task<bool> Create(CustomerCreateDto createDto)
        {
            try
            {
                CustomerEntity entity = createDto.FromCreateDtoToEntity();
                await _databaseContext.Customer.AddAsync(entity);
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(CustomerUpdateDto updateDto)
        {
            try
            {
                var existingEntity = await _databaseContext.Customer.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
                if (existingEntity is null)
                    return false;

                existingEntity.PersonalIdNumber = updateDto.PersonalIdNumber.Trim();
                existingEntity.Name = updateDto.Name.Trim();
                existingEntity.ProvinceId = updateDto.ProvinceId;
                existingEntity.CantonId = updateDto.CantonId;
                existingEntity.DistrictId = updateDto.DistrictId;
                existingEntity.Address = updateDto.Address.Trim();
                existingEntity.Email = updateDto.Email.Trim();
                existingEntity.PhoneNumber = updateDto.PhoneNumber.Trim();
                existingEntity.ContactPreference = updateDto.ContactPreference;

                _databaseContext.Entry(existingEntity).State = EntityState.Modified;
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var entityToRemove = await _databaseContext.Customer.FirstOrDefaultAsync(x => x.Id == id);
            if (entityToRemove is null)
                return false;

            try
            {
                _databaseContext.Customer.Remove(entityToRemove);
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<CustomerDto?> GetById(Guid id)
        {
            var entity = await _databaseContext.Customer
                .Include(x => x.Province)
                .Include(x => x.Canton)
                .Include(x => x.District)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity?.ToDto();
        }

        public async Task<List<CustomerDto>> GetAll()
        {
            var entities = await _databaseContext.Customer
                .Include(x => x.Province)
                .Include(x => x.Canton)
                .Include(x => x.District)
                .ToListAsync();
            return entities.ToDtosList();
        }

        public async Task<List<CustomerDto>> Search(string query)
        {
            var result = await _databaseContext.Customer
                .Include(x => x.Province)
                .Include(x => x.Canton)
                .Include(x => x.District)
                .Where(x =>
                    x.Name.Contains(query) ||
                    x.PersonalIdNumber.Contains(query) ||
                    x.Email.Contains(query) ||
                    x.PhoneNumber.Contains(query))
                .ToListAsync();
            return result.ToDtosList();
        }

        public async Task<int> Count() => await _databaseContext.Customer.CountAsync();
    }

    #endregion CRUD
}