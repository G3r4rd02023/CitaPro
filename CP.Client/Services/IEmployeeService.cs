using CP.Shared.DTOs;

namespace CP.Client.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetByBusinessIdAsync(Guid businessId);
        Task<List<EmployeeDto>> GetActiveEmployeesAsync();
        Task<EmployeeDto?> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(CreateEmployeeDto employee);
        Task<bool> UpdateAsync(Guid id, UpdateEmployeeDto employee);
        Task<bool> DeleteAsync(Guid id);
    }
}
