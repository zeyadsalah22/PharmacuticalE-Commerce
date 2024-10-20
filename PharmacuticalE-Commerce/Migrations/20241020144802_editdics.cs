using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacuticalE_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class editdics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxDiscountAmount",
                table: "PromoCodes",
                newName: "Value");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "PromoCodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "PromoCodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UsageLimit",
                table: "PromoCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "PromoCodes");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "PromoCodes");

            migrationBuilder.DropColumn(
                name: "UsageLimit",
                table: "PromoCodes");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "PromoCodes",
                newName: "MaxDiscountAmount");
        }
    }
}
