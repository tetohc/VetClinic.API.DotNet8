using Proyecto2_JerryHurtado.API.Helpers;
using Proyecto2_JerryHurtado.API.Mappers;
using Proyecto2_JerryHurtado.API.Models.Dtos.Employee;
using Proyecto2_JerryHurtado.API.Models.Entities;
using Proyecto2_JerryHurtado.API.Models.Enums;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Services
{
    /// <summary>
    /// Servicio para gestionar operaciones relacionadas con empleados.
    /// </summary>
    public class EmployeeService : IService<EmployeeCreateDto, EmployeeUpdateDto, EmployeeDto>
    {
        #region Datos simulados - Empleados precargados

        private List<EmployeeEntity> _entities = new()
        {
            new EmployeeEntity
            {
                Id = Guid.NewGuid(),
                PersonalIdNumber = "1-2345-6789",
                Birthdate = new DateOnly(1990, 1, 1),
                HireDate = new DateOnly(2020, 1, 1),
                DailySalary = 15000,
                TerminationDate = new DateOnly(2025, 12, 31),
                Type = (int)EmployeeType.Veterinarian
            },
            new EmployeeEntity
            {
                Id = Guid.NewGuid(),
                PersonalIdNumber = "2-9876-5432",
                Birthdate = new DateOnly(1985, 5, 15),
                HireDate = new DateOnly(2018, 3, 10),
                DailySalary = 18000,
                TerminationDate = new DateOnly(2026, 6, 30),
                Type = (int)EmployeeType.Administrative
            },
            new EmployeeEntity
            {
                Id = Guid.NewGuid(),
                PersonalIdNumber = "3-1122-3344",
                Birthdate = new DateOnly(1992, 8, 22),
                HireDate = new DateOnly(2021, 7, 1),
                DailySalary = 14000,
                TerminationDate = new DateOnly(2025, 11, 15),
                Type = (int)EmployeeType.Groomer
            },
            new EmployeeEntity
            {
                Id = Guid.NewGuid(),
                PersonalIdNumber = "4-5566-7788",
                Birthdate = new DateOnly(1995, 12, 5),
                HireDate = new DateOnly(2019, 9, 20),
                DailySalary = 16000,
                TerminationDate = new DateOnly(2024, 3, 31),
                Type = (int)EmployeeType.Assistant
            }
        };

        #endregion Datos simulados - Empleados precargados

        #region CRUD

        public bool Create(EmployeeCreateDto createDto)
        {
            try
            {
                var entity = createDto.FromCreateDtoToEntity();
                _entities.Add(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(EmployeeUpdateDto updateDto)
        {
            try
            {
                var existingEntity = _entities.FirstOrDefault(x => x.Id == updateDto.Id);
                if (existingEntity is null)
                    return false;

                existingEntity.PersonalIdNumber = updateDto.PersonalIdNumber.Trim();
                existingEntity.Birthdate = updateDto.Birthdate;
                existingEntity.HireDate = updateDto.HireDate;
                existingEntity.DailySalary = updateDto.DailySalary;
                existingEntity.TerminationDate = updateDto.TerminationDate;
                existingEntity.Type = updateDto.Type;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            var entityToRemove = _entities.FirstOrDefault(x => x.Id == id);
            if (entityToRemove is null)
                return false;
            return _entities.Remove(entityToRemove);
        }

        public EmployeeDto? GetById(Guid id) => _entities.FirstOrDefault(x => x.Id == id)?.ToDto();

        public List<EmployeeDto> GetAll() => _entities.ToDtosList();

        public List<EmployeeDto> Search(string query)
        {
            return _entities
                .Where(e => e.PersonalIdNumber.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                        EnumExtensions.GetDisplayName((EmployeeType)e.Type).Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToDtosList();
        }

        public int Count() => _entities.Count;

        #endregion CRUD
    }
}