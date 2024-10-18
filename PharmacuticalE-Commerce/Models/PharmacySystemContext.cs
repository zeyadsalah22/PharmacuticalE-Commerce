﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Viewmodels;
using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Models;

public partial class PharmacySystemContext : IdentityDbContext<User>
{
    public PharmacySystemContext()
    {
    }

    public PharmacySystemContext(DbContextOptions<PharmacySystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<PromoCode> PromoCodes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SalaryLog> SalaryLogs { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<ShippingAddress> ShippingAddresses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCard> UserCards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=PharmacySystem;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__Attendan__D825195E107E2F41");

            entity.ToTable("Attendance");

            entity.HasIndex(e => e.BranchId, "IX_Attendance_branchId");

            entity.HasIndex(e => e.EmployeeId, "IX_Attendance_employeeId");

            entity.HasIndex(e => e.ShiftId, "IX_Attendance_shiftId");

            entity.Property(e => e.RecordId).HasColumnName("recordId");
            entity.Property(e => e.AttendedAt)
                .HasColumnType("datetime")
                .HasColumnName("attendedAt");
            entity.Property(e => e.BranchId).HasColumnName("branchId");
            entity.Property(e => e.EmployeeId).HasColumnName("employeeId");
            entity.Property(e => e.LeftAt)
                .HasColumnType("datetime")
                .HasColumnName("leftAt");
            entity.Property(e => e.ShiftId).HasColumnName("shiftId");

            entity.HasOne(d => d.Branch).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attendanc__branc__3D5E1FD2");

            entity.HasOne(d => d.Employee).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attendanc__emplo__3B75D760");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("PK__Branch__751EBD5F933FEB49");

            entity.ToTable("Branch");

            entity.Property(e => e.BranchId).HasColumnName("branchId");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__415B03B8E7458263");

            entity.ToTable("Cart");

            entity.HasIndex(e => e.UserId, "IX_Cart_userId");

            entity.Property(e => e.CartId).HasColumnName("cartId");

            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__userId__5165187F");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => new { e.CartId, e.ProductId }).HasName("PK__CartItem__F38A0EAE461C9600");

            entity.ToTable("CartItem");

            entity.HasIndex(e => e.ProductId, "IX_CartItem_productId");

            entity.Property(e => e.CartId).HasColumnName("cartId");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.IsSelected)
                .HasDefaultValue(false)
                .HasColumnName("isSelected");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartItem__cartId__656C112C");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartItem__produc__66603565");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__23CAF1D854A9661D");

            entity.ToTable("Category");

            entity.HasIndex(e => e.ParentCategoryId, "IX_Category_parentCategoryId");

            entity.HasIndex(e => e.Name, "UQ__Category__72E12F1B99F0194C").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ParentCategoryId).HasColumnName("parentCategoryId");

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
                .HasForeignKey(d => d.ParentCategoryId)
                .HasConstraintName("FK__Category__parent__412EB0B6");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__D2130A66C0C1F387");

            entity.ToTable("Discount");

            entity.Property(e => e.DiscountId).HasColumnName("discountId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("startDate");
            entity.Property(e => e.ValuePct)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("valuePct");

            entity.HasMany(d => d.Categories).WithMany(p => p.Discounts)
                .UsingEntity<Dictionary<string, object>>(
                    "CategoryDiscount",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CategoryD__categ__01142BA1"),
                    l => l.HasOne<Discount>().WithMany()
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CategoryD__disco__00200768"),
                    j =>
                    {
                        j.HasKey("DiscountId", "CategoryId").HasName("PK__Category__402FA57B117933E4");
                        j.ToTable("CategoryDiscount");
                        j.HasIndex(new[] { "CategoryId" }, "IX_CategoryDiscount_categoryId");
                        j.IndexerProperty<int>("DiscountId").HasColumnName("discountId");
                        j.IndexerProperty<int>("CategoryId").HasColumnName("categoryId");
                    });

            entity.HasMany(d => d.Products).WithMany(p => p.Discounts)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductDiscount",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductDi__produ__7D439ABD"),
                    l => l.HasOne<Discount>().WithMany()
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductDi__disco__7C4F7684"),
                    j =>
                    {
                        j.HasKey("DiscountId", "ProductId").HasName("PK__ProductD__60C20770C507E758");
                        j.ToTable("ProductDiscount");
                        j.HasIndex(new[] { "ProductId" }, "IX_ProductDiscount_productId");
                        j.IndexerProperty<int>("DiscountId").HasColumnName("discountId");
                        j.IndexerProperty<int>("ProductId").HasColumnName("productId");
                    });
        });


        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C134C9C190EC8A1A");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.BranchId, "IX_Employee_branchId");

            entity.HasIndex(e => e.RoleId, "IX_Employee_roleId");

            entity.HasIndex(e => e.Email, "UQ__Employee__AB6E6164DA1FA5B2").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("employeeId");
            entity.Property(e => e.BranchId).HasColumnName("branchId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstDay)
                .HasColumnType("datetime")
                .HasColumnName("firstDay");
            entity.Property(e => e.Fname)
                .HasMaxLength(50)
                .HasColumnName("fname");
            entity.Property(e => e.Lname)
                .HasMaxLength(50)
                .HasColumnName("lname");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.Salary)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salary");
            entity.Property(e => e.ShiftId).HasColumnName("shiftId");

            entity.HasOne(d => d.Branch).WithMany(p => p.Employees)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__branch__2F10007B");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__roleId__2E1BDC42");

            entity.HasOne(d => d.Shift).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Shift");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__0809335DE1283F0A");

            entity.ToTable("Order");

            entity.HasIndex(e => e.ShippingAddressId, "IX_Order_addressId");

            entity.HasIndex(e => e.CartId, "IX_Order_cartId");

            entity.HasIndex(e => e.UserId, "IX_Order_userId");

            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.ShippingAddressId).HasColumnName("addressId");
            entity.Property(e => e.CartId).HasColumnName("cartId");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("orderDate");
            entity.Property(e => e.PromoCode)
                .HasMaxLength(50)
                .HasColumnName("promoCode");
            entity.Property(e => e.ShippingPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("shippingPrice");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalAmount");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.ShippingAddress).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShippingAddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__addressId__70DDC3D8");

            entity.HasOne(d => d.Cart).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__cartId__6EF57B66");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__userId__6FE99F9F");
        });


        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__2D10D16AFD65A980");

            entity.ToTable("Product");

            entity.HasIndex(e => e.CategoryId, "IX_Product_categoryId");

            entity.HasIndex(e => e.SerialNumber, "UQ__Product__3304A095B7795A78").IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Photo)
                .HasMaxLength(255)
                .HasColumnName("photo");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("serialNumber");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__categor__44FF419A");
        });


        modelBuilder.Entity<PromoCode>(entity =>
        {
            entity.HasKey(e => e.PromoCode1).HasName("PK__PromoCod__C7120D046F08FC51");

            entity.ToTable("PromoCode");

            entity.HasIndex(e => e.DiscountId, "IX_PromoCode_discountId");

            entity.Property(e => e.PromoCode1)
                .HasMaxLength(50)
                .HasColumnName("promoCode");
            entity.Property(e => e.DiscountId).HasColumnName("discountId");
            entity.Property(e => e.MaxDiscountAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("maxDiscountAmount");
            entity.Property(e => e.MinOrderAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("minOrderAmount");

            entity.HasOne(d => d.Discount).WithMany(p => p.PromoCodes)
                .HasForeignKey(d => d.DiscountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PromoCode__disco__03F0984C");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__CD98462A06F08C1E");

            entity.ToTable("Role");

            entity.HasIndex(e => e.Title, "UQ__Role__E52A1BB3D70C0FA2").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<SalaryLog>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__SalaryLo__D825195E34536BF0");

            entity.ToTable("SalaryLog");

            entity.HasIndex(e => e.EmployeeId, "IX_SalaryLog_employeeId");

            entity.Property(e => e.RecordId).HasColumnName("recordId");
            entity.Property(e => e.ChangeType)
                .HasMaxLength(10)
                .HasColumnName("changeType");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.EmployeeId).HasColumnName("employeeId");
            entity.Property(e => e.IsPermanent).HasColumnName("isPermanent");
            entity.Property(e => e.Value)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("value");

            entity.HasOne(d => d.Employee).WithMany(p => p.SalaryLogs)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SalaryLog__emplo__32E0915F");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.ShiftId).HasName("PK__Shift__F2F06B02C04E4CA5");

            entity.ToTable("Shift");

            entity.Property(e => e.ShiftId).HasColumnName("shiftId");
            entity.Property(e => e.FromTime).HasColumnName("fromTime");
            entity.Property(e => e.ToTime).HasColumnName("toTime");
        });

        modelBuilder.Entity<ShippingAddress>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Shipping__26A111AD63DA2C05");

            entity.ToTable("ShippingAddress");

            entity.HasIndex(e => e.UserId, "IX_ShippingAddress_userId");

            entity.Property(e => e.AddressId).HasColumnName("addressId");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.IsDefault)
                .HasDefaultValue(false)
                .HasColumnName("isDefault");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.ShippingAddresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShippingA__userI__6B24EA82");
        });

        modelBuilder.Entity<User>(entity =>
        {
            //entity.HasKey(e => e.UserId).HasName("PK__Users__CB9A1CFF7DF85AD0");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164CA5F8C8F").IsUnique();

            //entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            //entity.Property(e => e.Email)
            //    .HasMaxLength(100)
            //    .HasColumnName("email");
            entity.Property(e => e.Fname)
                .HasMaxLength(50)
                .HasColumnName("fname");
            entity.Property(e => e.Lname)
                .HasMaxLength(50)
                .HasColumnName("lname");
            //entity.Property(e => e.Password)
            //.HasMaxLength(100)
            //.HasColumnName("password");
        });

        modelBuilder.Entity<UserCard>(entity =>
        {
            entity.HasKey(e => new { e.CardId, e.CardNo }).HasName("PK__UserCard__E98DAD82B96B04DE");

            entity.ToTable("UserCard");

            entity.HasIndex(e => e.UserId, "IX_UserCard_userId");

            entity.HasIndex(e => e.CardNo, "UQ__UserCard__4D66913A9FD915EA").IsUnique();

            entity.Property(e => e.CardId)
                .ValueGeneratedOnAdd()
                .HasColumnName("cardId");
            entity.Property(e => e.CardNo)
                .HasMaxLength(20)
                .HasColumnName("cardNo");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("datetime")
                .HasColumnName("expirationDate");
            entity.Property(e => e.HolderName)
                .HasMaxLength(100)
                .HasColumnName("holderName");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.UserCards)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserCard__userId__75A278F5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<PharmacuticalE_Commerce.Viewmodels.RegisterViewModel> RegisterViewModel { get; set; } = default!;

public DbSet<PharmacuticalE_Commerce.Viewmodels.LoginViewModel> LoginViewModel { get; set; } = default!;

public DbSet<PharmacuticalE_Commerce.Models.UserMetaData> UserMetaData { get; set; } = default!;
}
