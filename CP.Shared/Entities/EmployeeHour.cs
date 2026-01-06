using System;
using System.ComponentModel.DataAnnotations;

namespace CP.Shared.Entities
{
    public class EmployeeHour
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EmployeeId { get; set; }
        public int DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        // Navigation property
        public Employee? Employee { get; set; }
    }
}
