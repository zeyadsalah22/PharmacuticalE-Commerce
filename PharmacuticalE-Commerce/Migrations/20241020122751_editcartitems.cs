using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacuticalE_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class editcartitems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isSelected",
                table: "CartItem");

            migrationBuilder.AddColumn<decimal>(
                name: "FinalPrice",
                table: "CartItem",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalPrice",
                table: "CartItem");

            migrationBuilder.AddColumn<bool>(
                name: "isSelected",
                table: "CartItem",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
