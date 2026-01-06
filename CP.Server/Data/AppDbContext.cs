using Microsoft.EntityFrameworkCore;
using CP.Shared.Entities;
using CP.Shared.Enums;

namespace CP.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<BusinessHour> BusinessHours { get; set; }
        public DbSet<EmployeeHour> EmployeeHours { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Business -> User (Owner)
            modelBuilder.Entity<Business>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Employee -> Business
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Business)
                .WithMany()
                .HasForeignKey(e => e.BusinessId)
                .OnDelete(DeleteBehavior.Cascade);

            // BusinessHour -> Business
            modelBuilder.Entity<BusinessHour>()
                .HasOne(bh => bh.Business)
                .WithMany(b => b.BusinessHours)
                .HasForeignKey(bh => bh.BusinessId)
                .OnDelete(DeleteBehavior.Cascade);

            // EmployeeHour -> Employee
            modelBuilder.Entity<EmployeeHour>()
                .HasOne(eh => eh.Employee)
                .WithMany()
                .HasForeignKey(eh => eh.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Service -> Business
            modelBuilder.Entity<Service>()
                .HasOne(s => s.Business)
                .WithMany()
                .HasForeignKey(s => s.BusinessId)
                .OnDelete(DeleteBehavior.Cascade);

            // Reservation configuration
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Business)
                .WithMany()
                .HasForeignKey(r => r.BusinessId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Employee)
                .WithMany()
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Service)
                .WithMany()
                .HasForeignKey(r => r.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Decimal Precision
            modelBuilder.Entity<Service>()
                .Property(s => s.Price)
                .HasColumnType("decimal(18,2)");

            // Seed Super Admin
            var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var passwordHash = "$2a$11$/RfwtrZ6ySzjmMS6pUMpzuueQl1mzk2f6D48us/y0k23kpqSvf6Zu"; // Hashed "tecno123"

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminId,
                FullName = "super admin",
                Email = "tecnologershn@gmail.com",
                Password = passwordHash,
                Role = Role.Admin,
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
        }
    }
}
