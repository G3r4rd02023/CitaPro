using System;

namespace CP.Shared.DTOs
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Guid BusinessId { get; set; }
    }

    public class CreateEmployeeDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public Guid BusinessId { get; set; }
    }

    public class UpdateEmployeeDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
