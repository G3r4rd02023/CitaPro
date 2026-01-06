using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CP.Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedSuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IntDurationMinutes",
                table: "Services",
                newName: "DurationMinutes");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "IsActive", "Password", "Role" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 1, 6, 16, 3, 12, 444, DateTimeKind.Utc).AddTicks(7288), "tecnologershn@gmail.com", "super admin", true, "$2a$11$y5qkdYEHuHtKQX/URE1X7ue9/S4.n3y0WuVk13eQghIhMj81YU96m", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.RenameColumn(
                name: "DurationMinutes",
                table: "Services",
                newName: "IntDurationMinutes");
        }
    }
}
