using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories;
using PharmacuticalE_Commerce.Repositories.Implements;
using PharmacuticalE_Commerce.Repositories.Interfaces;
using Stripe;

namespace PharmacuticalE_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<PharmacySystemContext>(options =>
                options.UseSqlServer(connectionString));
            
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
            
            builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>().AddDefaultUI()
                .AddEntityFrameworkStores<PharmacySystemContext>();
            
            builder.Services.ConfigureApplicationCookie(options =>
			{
				options.AccessDeniedPath = "/UserAuth/AccessDenied";
                options.LoginPath = "/UserAuth/Login";

            });

			builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            builder.Services.AddScoped<IProductRepository,ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
            builder.Services.AddScoped<IBranchRepository, BranchRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IAttendanceRepository,AttendanceRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IShippingAddressRepository, ShippingAddressRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "checkout",
                pattern: "checkout/{action=Index}/{id?}",
                defaults: new { controller = "Checkout", action = "Index" });
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<User>>();

                // Ensure roles exist
                var roles = new[] { "Admin", "HR", "Moderator", "Customer" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                // Ensure admin user exists
                var adminUsers = await userManager.GetUsersInRoleAsync("Admin");
                if (!adminUsers.Any())
                {
                    var adminUser = new User
                    {
                        UserName = "Admin",
                        Email = "admin@example.com",
                        PhoneNumber = "1234567890",
                        Fname = "Pharma",
                        Lname = "Ease"
                    };
                    var password = "Admin@12345";

                    var result = await userManager.CreateAsync(adminUser, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                    else
                    {
                        throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
                    }
                }
            }
            app.Run();
        }
    }
}
