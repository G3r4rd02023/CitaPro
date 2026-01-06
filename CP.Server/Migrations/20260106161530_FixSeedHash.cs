using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CP.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Password",
                value: "$2a$11$/RfwtrZ6ySzjmMS6pUMpzuueQl1mzk2f6D48us/y0k23kpqSvf6Zu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Password",
                value: "$2a$11$BgD2eb8pEE7WsrM14TfXjeE8bpTrJOg69au/p/dF7ug.rATARLBYC");
        }
    }
}
