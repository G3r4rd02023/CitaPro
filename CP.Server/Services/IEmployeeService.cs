using CP.Shared.DTOs;

namespace CP.Server.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<IEnumerable<EmployeeDto>> GetByBusinessIdAsync(Guid businessId);
        Task<IEnumerable<EmployeeDto>> GetActiveAsync();
        Task<EmployeeDto?> GetByIdAsync(Guid id);
        Task<EmployeeDto> CreateAsync(CreateEmployeeDto createDto);
        Task<EmployeeDto?> UpdateAsync(Guid id, UpdateEmployeeDto updateDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
