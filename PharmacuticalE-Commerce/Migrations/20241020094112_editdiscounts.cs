using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacuticalE_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class editdiscounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key and the PromoCode table
            migrationBuilder.DropForeignKey(
                name: "FK__PromoCode__disco__03F0984C",
                table: "PromoCode");

            // Drop unused tables
            migrationBuilder.DropTable(
                name: "CategoryDiscount");
            migrationBuilder.DropTable(
                name: "LoginViewModel");
            migrationBuilder.DropTable(
                name: "ProductDiscount");
            migrationBuilder.DropTable(
                name: "RegisterViewModel");

            // Drop primary key from the PromoCode table
            migrationBuilder.DropPrimaryKey(
                name: "PK__PromoCod__C7120D046F08FC51",
                table: "PromoCode");

            // Drop existing index
            migrationBuilder.DropIndex(
                name: "IX_PromoCode_discountId",
                table: "PromoCode");

            // Rename PromoCode table to PromoCodes
            migrationBuilder.RenameTable(
                name: "PromoCode",
                newName: "PromoCodes");

            // Rename columns
            migrationBuilder.RenameColumn(
                name: "minOrderAmount",
                table: "PromoCodes",
                newName: "MinOrderAmount");
            migrationBuilder.RenameColumn(
                name: "maxDiscountAmount",
                table: "PromoCodes",
                newName: "MaxDiscountAmount");
            migrationBuilder.RenameColumn(
                name: "promoCode",
                table: "PromoCodes",
                newName: "PromoCode1");

            // Add new columns to Product and Branch tables
            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Product",
                type: "int",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Branch",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Branch",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            // Check if PromoCodeId already exists before adding it
            migrationBuilder.AddColumn<int>(
                    name: "PromoCodeId",
                    table: "PromoCodes",
                    type: "int",
                    nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"); // Add IDENTITY property

            // Alter columns
            migrationBuilder.AlterColumn<decimal>(
                name: "MinOrderAmount",
                table: "PromoCodes",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);
            migrationBuilder.AlterColumn<decimal>(
                name: "MaxDiscountAmount",
                table: "PromoCodes",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);
            migrationBuilder.AlterColumn<string>(
                name: "PromoCode1",
                table: "PromoCodes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            // Set primary key for PromoCodes
            migrationBuilder.AddPrimaryKey(
                name: "PK_PromoCodes",
                table: "PromoCodes",
                column: "PromoCodeId");

            // Create index on Product table
            migrationBuilder.CreateIndex(
                name: "IX_Product_DiscountId",
                table: "Product",
                column: "DiscountId",
                unique: true,
                filter: "[DiscountId] IS NOT NULL");

            // Add foreign key constraint
            migrationBuilder.AddForeignKey(
                name: "FK_Product_Discount_DiscountId",
                table: "Product",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "discountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Implement logic to reverse the migration if needed
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Discount_DiscountId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_DiscountId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PromoCodes",
                table: "PromoCodes");

            migrationBuilder.DropColumn(
                name: "PromoCodeId",
                table: "PromoCodes");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Branch");

            migrationBuilder.RenameTable(
                name: "PromoCodes",
                newName: "PromoCode");

            migrationBuilder.RenameColumn(
                name: "MinOrderAmount",
                table: "PromoCode",
                newName: "minOrderAmount");

            migrationBuilder.RenameColumn(
                name: "MaxDiscountAmount",
                table: "PromoCode",
                newName: "maxDiscountAmount");

            migrationBuilder.RenameColumn(
                name: "PromoCode1",
                table: "PromoCode",
                newName: "promoCode");

            migrationBuilder.AddColumn<int>(
                name: "discountId",
                table: "PromoCode",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__PromoCod__C7120D046F08FC51",
                table: "PromoCode",
                column: "discountId");

            // Recreate any dropped tables if necessary
        }
    }
}
