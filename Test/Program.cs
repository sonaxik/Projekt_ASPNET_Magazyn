using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Test.Areas.Identity.Data;
using Test.Models;

namespace Test
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("ApplicationDBContextConnection")
                ?? throw new InvalidOperationException("Connection string 'ApplicationDBContextConnection' not found.");

            builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

                context.Database.Migrate();

                var roles = new[] { "Admin", "User" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }

                string adminEmail = "admin@admin.com";
                string adminPassword = "Admin1234!";
                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var adminUser = new IdentityUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(adminUser, adminPassword);
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }

                string userEmail = "user@user.com";
                string userPassword = "User1234!";
                if (await userManager.FindByEmailAsync(userEmail) == null)
                {
                    var regularUser = new IdentityUser
                    {
                        UserName = userEmail,
                        Email = userEmail,
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(regularUser, userPassword);
                    await userManager.AddToRoleAsync(regularUser, "User");
                }

                if (!context.Asortyment.Any())
                {
                    context.Asortyment.AddRange(
                        new Asortyment { Name = "Smartphone Galaxy X10", Brand = "Samsung", Price = 999.99m, Description = "Nowoczesny smartfon z ekranem AMOLED 6.5 cala, 128GB pamiêci i potrójnym aparatem.", Quantity = 100 },
                        new Asortyment { Name = "Laptop ProBook 450", Brand = "HP", Price = 2499.99m, Description = "Laptop biznesowy z procesorem Intel i7, 16GB RAM i dyskiem SSD 512GB.", Quantity = 50 },
                        new Asortyment { Name = "S³uchawki bezprzewodowe AirPods", Brand = "Apple", Price = 899.00m, Description = "S³uchawki bezprzewodowe z aktywn¹ redukcj¹ szumów i doskona³¹ jakoœci¹ dŸwiêku.", Quantity = 200 },
                        new Asortyment { Name = "Telewizor OLED 55CX", Brand = "LG", Price = 4999.00m, Description = "Telewizor OLED 55 cali z obs³ug¹ 4K HDR, technologi¹ Dolby Vision i systemem Smart TV.", Quantity = 20 },
                        new Asortyment { Name = "Ekspres do kawy Barista Pro", Brand = "DeLonghi", Price = 1599.99m, Description = "Automatyczny ekspres do kawy z m³ynkiem, ekranem LCD i mo¿liwoœci¹ przygotowania cappuccino.", Quantity = 30 }
                    );

                    await context.SaveChangesAsync();
                }
            }

            app.Run();
        }
    }
}