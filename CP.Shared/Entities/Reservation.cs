using System;
using System.ComponentModel.DataAnnotations;
using CP.Shared.Enums;

namespace CP.Shared.Entities
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid BusinessId { get; set; }
        public Guid UserId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Business? Business { get; set; }
        public User? User { get; set; } // Client
        public Employee? Employee { get; set; }
        public Service? Service { get; set; }
    }
}
