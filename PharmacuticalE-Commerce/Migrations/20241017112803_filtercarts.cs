using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacuticalE_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class filtercarts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorInPrescription");

            migrationBuilder.DropTable(
                name: "PAV");

            migrationBuilder.DropTable(
                name: "ProductPrice");

            migrationBuilder.DropTable(
                name: "ShoppingCart");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropTable(
                name: "ProductAttribute");

            migrationBuilder.DropColumn(
                name: "productName",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "type",
                table: "Cart");

            migrationBuilder.AddColumn<string>(
                name: "Fname",
                table: "RegisterViewModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lname",
                table: "RegisterViewModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "RegisterViewModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<bool>(
                name: "isSelected",
                table: "CartItem",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Cart",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Cart",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LoginViewModel",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RememberMe = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginViewModel");

            migrationBuilder.DropColumn(
                name: "Fname",
                table: "RegisterViewModel");

            migrationBuilder.DropColumn(
                name: "Lname",
                table: "RegisterViewModel");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "RegisterViewModel");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Cart");

            migrationBuilder.AlterColumn<bool>(
                name: "isSelected",
                table: "CartItem",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "productName",
                table: "CartItem",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Cart",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    doctorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Doctor__722484769DF35E54", x => x.doctorId);
                });

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    prescriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cartId = table.Column<int>(type: "int", nullable: false),
                    employeeId = table.Column<int>(type: "int", nullable: false),
                    photo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Prescrip__7920FC2498C9F50F", x => x.prescriptionId);
                    table.ForeignKey(
                        name: "FK__Prescript__cartI__5629CD9C",
                        column: x => x.cartId,
                        principalTable: "Cart",
                        principalColumn: "cartId");
                    table.ForeignKey(
                        name: "FK__Prescript__emplo__571DF1D5",
                        column: x => x.employeeId,
                        principalTable: "Employee",
                        principalColumn: "employeeId");
                });

            migrationBuilder.CreateTable(
                name: "ProductAttribute",
                columns: table => new
                {
                    attributeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductA__03B803F048206992", x => x.attributeId);
                });

            migrationBuilder.CreateTable(
                name: "ProductPrice",
                columns: table => new
                {
                    priceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(type: "int", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductP__366E4CC285ECEDD9", x => x.priceId);
                    table.ForeignKey(
                        name: "FK__ProductPr__produ__47DBAE45",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "productId");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                columns: table => new
                {
                    shoppingCartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cartId = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shopping__21DFC906F465567E", x => x.shoppingCartId);
                    table.ForeignKey(
                        name: "FK__ShoppingC__cartI__60A75C0F",
                        column: x => x.cartId,
                        principalTable: "Cart",
                        principalColumn: "cartId");
                });

            migrationBuilder.CreateTable(
                name: "DoctorInPrescription",
                columns: table => new
                {
                    prescriptionId = table.Column<int>(type: "int", nullable: false),
                    doctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DoctorIn__0E02B463C56294AE", x => new { x.prescriptionId, x.doctorId });
                    table.ForeignKey(
                        name: "FK__DoctorInP__docto__5CD6CB2B",
                        column: x => x.doctorId,
                        principalTable: "Doctor",
                        principalColumn: "doctorId");
                    table.ForeignKey(
                        name: "FK__DoctorInP__presc__5BE2A6F2",
                        column: x => x.prescriptionId,
                        principalTable: "Prescription",
                        principalColumn: "prescriptionId");
                });

            migrationBuilder.CreateTable(
                name: "PAV",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false),
                    attributeId = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PAV__3D2B51558DCFC313", x => new { x.productId, x.attributeId });
                    table.ForeignKey(
                        name: "FK__PAV__attributeId__4D94879B",
                        column: x => x.attributeId,
                        principalTable: "ProductAttribute",
                        principalColumn: "attributeId");
                    table.ForeignKey(
                        name: "FK__PAV__productId__4CA06362",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "productId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorInPrescription_doctorId",
                table: "DoctorInPrescription",
                column: "doctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PAV_attributeId",
                table: "PAV",
                column: "attributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_cartId",
                table: "Prescription",
                column: "cartId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_employeeId",
                table: "Prescription",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrice_productId",
                table: "ProductPrice",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_cartId",
                table: "ShoppingCart",
                column: "cartId");
        }
    }
}
