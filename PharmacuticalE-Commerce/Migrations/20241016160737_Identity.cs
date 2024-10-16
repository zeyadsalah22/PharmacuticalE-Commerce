using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacuticalE_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class Identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    fname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    lname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__CB9A1CFF7DF85AD0", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    branchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Branch__751EBD5F933FEB49", x => x.branchId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    parentCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__23CAF1D854A9661D", x => x.categoryId);
                    table.ForeignKey(
                        name: "FK__Category__parent__412EB0B6",
                        column: x => x.parentCategoryId,
                        principalTable: "Category",
                        principalColumn: "categoryId");
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    discountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    valuePct = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    startDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Discount__D2130A66C0C1F387", x => x.discountId);
                });

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
                name: "Role",
                columns: table => new
                {
                    roleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__CD98462A06F08C1E", x => x.roleId);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    shiftId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fromTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    toTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shift__F2F06B02C04E4CA5", x => x.shiftId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    cartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cart__415B03B8E7458263", x => x.cartId);
                    table.ForeignKey(
                        name: "FK__Cart__userId__5165187F",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "ShippingAddress",
                columns: table => new
                {
                    addressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    isDefault = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shipping__26A111AD63DA2C05", x => x.addressId);
                    table.ForeignKey(
                        name: "FK__ShippingA__userI__6B24EA82",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "UserCard",
                columns: table => new
                {
                    cardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cardNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    holderName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    expirationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserCard__E98DAD82B96B04DE", x => new { x.cardId, x.cardNo });
                    table.ForeignKey(
                        name: "FK__UserCard__userId__75A278F5",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    serialNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    photo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__2D10D16AFD65A980", x => x.productId);
                    table.ForeignKey(
                        name: "FK__Product__categor__44FF419A",
                        column: x => x.categoryId,
                        principalTable: "Category",
                        principalColumn: "categoryId");
                });

            migrationBuilder.CreateTable(
                name: "CategoryDiscount",
                columns: table => new
                {
                    discountId = table.Column<int>(type: "int", nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__402FA57B117933E4", x => new { x.discountId, x.categoryId });
                    table.ForeignKey(
                        name: "FK__CategoryD__categ__01142BA1",
                        column: x => x.categoryId,
                        principalTable: "Category",
                        principalColumn: "categoryId");
                    table.ForeignKey(
                        name: "FK__CategoryD__disco__00200768",
                        column: x => x.discountId,
                        principalTable: "Discount",
                        principalColumn: "discountId");
                });

            migrationBuilder.CreateTable(
                name: "PromoCode",
                columns: table => new
                {
                    promoCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    discountId = table.Column<int>(type: "int", nullable: false),
                    minOrderAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    maxDiscountAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PromoCod__C7120D046F08FC51", x => x.promoCode);
                    table.ForeignKey(
                        name: "FK__PromoCode__disco__03F0984C",
                        column: x => x.discountId,
                        principalTable: "Discount",
                        principalColumn: "discountId");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    employeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    lname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    roleId = table.Column<int>(type: "int", nullable: false),
                    branchId = table.Column<int>(type: "int", nullable: false),
                    salary = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    firstDay = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    shiftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__C134C9C190EC8A1A", x => x.employeeId);
                    table.ForeignKey(
                        name: "FK_Employee_Shift",
                        column: x => x.shiftId,
                        principalTable: "Shift",
                        principalColumn: "shiftId");
                    table.ForeignKey(
                        name: "FK__Employee__branch__2F10007B",
                        column: x => x.branchId,
                        principalTable: "Branch",
                        principalColumn: "branchId");
                    table.ForeignKey(
                        name: "FK__Employee__roleId__2E1BDC42",
                        column: x => x.roleId,
                        principalTable: "Role",
                        principalColumn: "roleId");
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
                name: "Order",
                columns: table => new
                {
                    orderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cartId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    addressId = table.Column<int>(type: "int", nullable: false),
                    orderDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    totalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    shippingPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    promoCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order__0809335DE1283F0A", x => x.orderId);
                    table.ForeignKey(
                        name: "FK__Order__addressId__70DDC3D8",
                        column: x => x.addressId,
                        principalTable: "ShippingAddress",
                        principalColumn: "addressId");
                    table.ForeignKey(
                        name: "FK__Order__cartId__6EF57B66",
                        column: x => x.cartId,
                        principalTable: "Cart",
                        principalColumn: "cartId");
                    table.ForeignKey(
                        name: "FK__Order__userId__6FE99F9F",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    cartId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    productName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    isSelected = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CartItem__F38A0EAE461C9600", x => new { x.cartId, x.productId });
                    table.ForeignKey(
                        name: "FK__CartItem__cartId__656C112C",
                        column: x => x.cartId,
                        principalTable: "Cart",
                        principalColumn: "cartId");
                    table.ForeignKey(
                        name: "FK__CartItem__produc__66603565",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "productId");
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

            migrationBuilder.CreateTable(
                name: "ProductDiscount",
                columns: table => new
                {
                    discountId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductD__60C20770C507E758", x => new { x.discountId, x.productId });
                    table.ForeignKey(
                        name: "FK__ProductDi__disco__7C4F7684",
                        column: x => x.discountId,
                        principalTable: "Discount",
                        principalColumn: "discountId");
                    table.ForeignKey(
                        name: "FK__ProductDi__produ__7D439ABD",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "productId");
                });

            migrationBuilder.CreateTable(
                name: "ProductPrice",
                columns: table => new
                {
                    priceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(type: "int", nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
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
                name: "Attendance",
                columns: table => new
                {
                    recordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeId = table.Column<int>(type: "int", nullable: false),
                    shiftId = table.Column<int>(type: "int", nullable: false),
                    branchId = table.Column<int>(type: "int", nullable: false),
                    attendedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    leftAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Attendan__D825195E107E2F41", x => x.recordId);
                    table.ForeignKey(
                        name: "FK__Attendanc__branc__3D5E1FD2",
                        column: x => x.branchId,
                        principalTable: "Branch",
                        principalColumn: "branchId");
                    table.ForeignKey(
                        name: "FK__Attendanc__emplo__3B75D760",
                        column: x => x.employeeId,
                        principalTable: "Employee",
                        principalColumn: "employeeId");
                });

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    prescriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    photo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    cartId = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    employeeId = table.Column<int>(type: "int", nullable: false),
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
                name: "SalaryLog",
                columns: table => new
                {
                    recordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeId = table.Column<int>(type: "int", nullable: false),
                    changeType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    isPermanent = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__AB6E6164CA5F8C8F",
                table: "AspNetUsers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_branchId",
                table: "Attendance",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_employeeId",
                table: "Attendance",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_shiftId",
                table: "Attendance",
                column: "shiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_userId",
                table: "Cart",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_productId",
                table: "CartItem",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_parentCategoryId",
                table: "Category",
                column: "parentCategoryId");

            migrationBuilder.CreateIndex(
                name: "UQ__Category__72E12F1B99F0194C",
                table: "Category",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDiscount_categoryId",
                table: "CategoryDiscount",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorInPrescription_doctorId",
                table: "DoctorInPrescription",
                column: "doctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_branchId",
                table: "Employee",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_roleId",
                table: "Employee",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_shiftId",
                table: "Employee",
                column: "shiftId");

            migrationBuilder.CreateIndex(
                name: "UQ__Employee__AB6E6164DA1FA5B2",
                table: "Employee",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_addressId",
                table: "Order",
                column: "addressId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_cartId",
                table: "Order",
                column: "cartId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_userId",
                table: "Order",
                column: "userId");

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
                name: "IX_Product_categoryId",
                table: "Product",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "UQ__Product__3304A095B7795A78",
                table: "Product",
                column: "serialNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscount_productId",
                table: "ProductDiscount",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrice_productId",
                table: "ProductPrice",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCode_discountId",
                table: "PromoCode",
                column: "discountId");

            migrationBuilder.CreateIndex(
                name: "UQ__Role__E52A1BB3D70C0FA2",
                table: "Role",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalaryLog_employeeId",
                table: "SalaryLog",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddress_userId",
                table: "ShippingAddress",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_cartId",
                table: "ShoppingCart",
                column: "cartId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "CategoryDiscount");

            migrationBuilder.DropTable(
                name: "DoctorInPrescription");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "PAV");

            migrationBuilder.DropTable(
                name: "ProductDiscount");

            migrationBuilder.DropTable(
                name: "ProductPrice");

            migrationBuilder.DropTable(
                name: "PromoCode");

            migrationBuilder.DropTable(
                name: "SalaryLog");

            migrationBuilder.DropTable(
                name: "ShoppingCart");

            migrationBuilder.DropTable(
                name: "UserCard");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropTable(
                name: "ShippingAddress");

            migrationBuilder.DropTable(
                name: "ProductAttribute");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
