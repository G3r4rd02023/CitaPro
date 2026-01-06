using CP.Server.Repositories;
using CP.Shared.DTOs;
using CP.Shared.Entities;

namespace CP.Server.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _repository.GetAllAsync();
            return employees.Select(MapToDto);
        }

        public async Task<IEnumerable<EmployeeDto>> GetByBusinessIdAsync(Guid businessId)
        {
            var employees = await _repository.GetByBusinessIdAsync(businessId);
            return employees.Select(MapToDto);
        }

        public async Task<IEnumerable<EmployeeDto>> GetActiveAsync()
        {
            var employees = await _repository.GetAllAsync();
            return employees.Where(e => e.IsActive).Select(MapToDto);
        }

        public async Task<EmployeeDto?> GetByIdAsync(Guid id)
        {
            var employee = await _repository.GetByIdAsync(id);
            return employee == null ? null : MapToDto(employee);
        }

        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto createDto)
        {
            var employee = new Employee
            {
                FullName = createDto.FullName,
                Specialty = createDto.Specialty,
                BusinessId = createDto.BusinessId,
                IsActive = true
            };
            var result = await _repository.CreateAsync(employee);
            return MapToDto(result);
        }

        public async Task<EmployeeDto?> UpdateAsync(Guid id, UpdateEmployeeDto updateDto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.FullName = updateDto.FullName;
            existing.Specialty = updateDto.Specialty;
            existing.IsActive = updateDto.IsActive;

            await _repository.UpdateAsync(existing);
            return MapToDto(existing);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        private static EmployeeDto MapToDto(Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Specialty = employee.Specialty,
                IsActive = employee.IsActive,
                BusinessId = employee.BusinessId
            };
        }
    }
}
