using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CP.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$BgD2eb8pEE7WsrM14TfXjeE8bpTrJOg69au/p/dF7ug.rATARLBYC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2026, 1, 6, 16, 3, 12, 444, DateTimeKind.Utc).AddTicks(7288), "$2a$11$y5qkdYEHuHtKQX/URE1X7ue9/S4.n3y0WuVk13eQghIhMj81YU96m" });
        }
    }
}
