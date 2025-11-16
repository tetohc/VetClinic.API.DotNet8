using Microsoft.EntityFrameworkCore;
using Proyecto2_JerryHurtado.API.Database;
using Proyecto2_JerryHurtado.API.Helpers;
using Proyecto2_JerryHurtado.API.Mappers;
using Proyecto2_JerryHurtado.API.Models.Dtos.Employee;
using Proyecto2_JerryHurtado.API.Models.Enums;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Services
{
    /// <summary>
    /// Servicio para gestionar operaciones relacionadas con empleados.
    /// </summary>
    public class EmployeeService : IService<EmployeeCreateDto, EmployeeUpdateDto, EmployeeDto>
    {
        private readonly DatabaseContext _databaseContext;

        public EmployeeService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        #region CRUD

        public async Task<bool> Create(EmployeeCreateDto createDto)
        {
            try
            {
                var entity = createDto.FromCreateDtoToEntity();
                await _databaseContext.Employee.AddAsync(entity);
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(EmployeeUpdateDto updateDto)
        {
            try
            {
                var existingEntity = await _databaseContext.Employee.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
                if (existingEntity is null)
                    return false;

                existingEntity.PersonalIdNumber = updateDto.PersonalIdNumber.Trim();
                existingEntity.Birthdate = updateDto.Birthdate;
                existingEntity.HireDate = updateDto.HireDate;
                existingEntity.DailySalary = updateDto.DailySalary;
                existingEntity.TerminationDate = updateDto.TerminationDate;
                existingEntity.Type = updateDto.Type;

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
            var entityToRemove = await _databaseContext.Employee.FirstOrDefaultAsync(x => x.Id == id);
            if (entityToRemove is null)
                return false;

            try
            {
                _databaseContext.Employee.Remove(entityToRemove);
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<EmployeeDto?> GetById(Guid id)
        {
            var entity = await _databaseContext.Employee.FirstOrDefaultAsync(x => x.Id == id);
            return entity?.ToDto();
        }

        public async Task<List<EmployeeDto>> GetAll()
        {
            var entities = await _databaseContext.Employee.ToListAsync();
            return entities.ToDtosList();
        }

        public async Task<List<EmployeeDto>> Search(string query)
        {
            query = query.Trim();
            var result = await _databaseContext.Employee
                .Where(e => e.PersonalIdNumber.Contains(query))
                .ToListAsync();

            if (!result.Any())
            {
                var allEmployees = await _databaseContext.Employee.ToListAsync();
                result = allEmployees
                    .Where(e => EnumExtensions.GetDisplayName((EmployeeType)e.Type)
                        .Contains(query, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            return result.ToDtosList();
        }

        public async Task<int> Count() => await _databaseContext.Employee.CountAsync();

        #endregion CRUD
    }
}