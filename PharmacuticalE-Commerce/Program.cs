using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories;
using PharmacuticalE_Commerce.Repositories.Implements;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<PharmacySystemContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<PharmacySystemContext>();
            builder.Services.AddControllersWithViews();

            // 34an mn3ml4 Rebuild kol ma n3dl el view
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            builder.Services.AddScoped<IProductRepository,ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
            builder.Services.AddScoped<IBranchRepository, BranchRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IAttendanceRepository,AttendanceRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();

            //builder.Services.AddIdentity<User, IdentityRole>()
            //    .AddEntityFrameworkStores<PharmacySystemContext>();

            //builder.Services.AddDbContext<PharmacySystemContext>(options =>
            //{
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            //});




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
