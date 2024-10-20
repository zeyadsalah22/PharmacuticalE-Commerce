using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacuticalE_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class removusermetadataa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Cart_UserMetaData_UserMetaDataUserId",
            //    table: "Cart");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Order_UserMetaData_UserMetaDataUserId",
            //    table: "Order");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ShippingAddress_UserMetaData_UserMetaDataUserId",
            //    table: "ShippingAddress");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserCard_UserMetaData_UserMetaDataUserId",
            //    table: "UserCard");

            //migrationBuilder.DropTable(
            //    name: "UserMetaData");

            //migrationBuilder.DropIndex(
            //    name: "IX_UserCard_UserMetaDataUserId",
            //    table: "UserCard");

            //migrationBuilder.DropIndex(
            //    name: "IX_ShippingAddress_UserMetaDataUserId",
            //    table: "ShippingAddress");

            //migrationBuilder.DropIndex(
            //    name: "IX_Order_UserMetaDataUserId",
            //    table: "Order");

            //migrationBuilder.DropIndex(
            //    name: "IX_Cart_UserMetaDataUserId",
            //    table: "Cart");

            //migrationBuilder.DropColumn(
            //    name: "UserMetaDataUserId",
            //    table: "UserCard");

            //migrationBuilder.DropColumn(
            //    name: "UserMetaDataUserId",
            //    table: "ShippingAddress");

            //migrationBuilder.DropColumn(
            //    name: "UserMetaDataUserId",
            //    table: "Order");

            //migrationBuilder.DropColumn(
            //    name: "UserMetaDataUserId",
            //    table: "Cart");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.AddColumn<int>(
        //        name: "UserMetaDataUserId",
        //        table: "UserCard",
        //        type: "int",
        //        nullable: true);

        //    migrationBuilder.AddColumn<int>(
        //        name: "UserMetaDataUserId",
        //        table: "ShippingAddress",
        //        type: "int",
        //        nullable: true);

        //    migrationBuilder.AddColumn<int>(
        //        name: "UserMetaDataUserId",
        //        table: "Order",
        //        type: "int",
        //        nullable: true);

        //    migrationBuilder.AddColumn<int>(
        //        name: "UserMetaDataUserId",
        //        table: "Cart",
        //        type: "int",
        //        nullable: true);

        //    migrationBuilder.CreateTable(
        //        name: "UserMetaData",
        //        columns: table => new
        //        {
        //            UserId = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
        //            Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            Fname = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            Lname = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_UserMetaData", x => x.UserId);
        //        });

        //    migrationBuilder.CreateIndex(
        //        name: "IX_UserCard_UserMetaDataUserId",
        //        table: "UserCard",
        //        column: "UserMetaDataUserId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_ShippingAddress_UserMetaDataUserId",
        //        table: "ShippingAddress",
        //        column: "UserMetaDataUserId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Order_UserMetaDataUserId",
        //        table: "Order",
        //        column: "UserMetaDataUserId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Cart_UserMetaDataUserId",
        //        table: "Cart",
        //        column: "UserMetaDataUserId");

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_Cart_UserMetaData_UserMetaDataUserId",
        //        table: "Cart",
        //        column: "UserMetaDataUserId",
        //        principalTable: "UserMetaData",
        //        principalColumn: "UserId");

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_Order_UserMetaData_UserMetaDataUserId",
        //        table: "Order",
        //        column: "UserMetaDataUserId",
        //        principalTable: "UserMetaData",
        //        principalColumn: "UserId");

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_ShippingAddress_UserMetaData_UserMetaDataUserId",
        //        table: "ShippingAddress",
        //        column: "UserMetaDataUserId",
        //        principalTable: "UserMetaData",
        //        principalColumn: "UserId");

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_UserCard_UserMetaData_UserMetaDataUserId",
        //        table: "UserCard",
        //        column: "UserMetaDataUserId",
        //        principalTable: "UserMetaData",
        //        principalColumn: "UserId");
        }
    }
}
