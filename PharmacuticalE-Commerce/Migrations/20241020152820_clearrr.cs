using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacuticalE_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class clearrr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaryLog");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalaryLog",
                columns: table => new
                {
                    recordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeId = table.Column<int>(type: "int", nullable: false),
                    changeType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    isPermanent = table.Column<bool>(type: "bit", nullable: false),
                    value = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SalaryLo__D825195E34536BF0", x => x.recordId);
                    table.ForeignKey(
                        name: "FK__SalaryLog__emplo__32E0915F",
                        column: x => x.employeeId,
                        principalTable: "Employee",
                        principalColumn: "employeeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalaryLog_employeeId",
                table: "SalaryLog",
                column: "employeeId");
        }
    }
}
