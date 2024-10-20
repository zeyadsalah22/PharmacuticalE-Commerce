using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacuticalE_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class clearr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCard");

            migrationBuilder.RenameColumn(
                name: "isDefault",
                table: "ShippingAddress",
                newName: "IsDefault");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDefault",
                table: "ShippingAddress",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDefault",
                table: "ShippingAddress",
                newName: "isDefault");

            migrationBuilder.AlterColumn<bool>(
                name: "isDefault",
                table: "ShippingAddress",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserCard",
                columns: table => new
                {
                    cardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cardNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    expirationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    holderName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserCard__E98DAD82B96B04DE", x => new { x.cardId, x.cardNo });
                    table.ForeignKey(
                        name: "FK__UserCard__userId__75A278F5",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCard_userId",
                table: "UserCard",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "UQ__UserCard__4D66913A9FD915EA",
                table: "UserCard",
                column: "cardNo",
                unique: true);
        }
    }
}
