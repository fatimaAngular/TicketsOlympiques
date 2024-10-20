using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketsJO.ConfigurationMapping;

//using MySql.Data.MySqlClient;
//using MySqlX.XDevAPI;
using TicketsJO.Data;
using TicketsJO.Models;

namespace TicketsJO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //------------------------- Probl�me de MariaDB --------------------------------
            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(connectionString)));

            ////--------------------------------------------------------------

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                 .AddRoles<IdentityRole>()
                 .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<ShoppingCart>(sp =>
           ShoppingCart.GetCart(sp.GetRequiredService<IHttpContextAccessor>().HttpContext,
                         sp.GetRequiredService<ApplicationDbContext>()));

            builder.Services.AddControllersWithViews();

            builder.Services.AddAutoMapper(typeof(MappingProfile));


            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Durée du verrouillage
                options.Lockout.MaxFailedAccessAttempts = 3; // Nombre maximum de tentatives avant verrouillage
                options.Lockout.AllowedForNewUsers = true; // Appliquer également pour les nouveaux utilisateurs
            });

            // Nécessaire pour accéder au HttpContext
            builder.Services.AddHttpContextAccessor();

            // Ajouter les sessions
            builder.Services.AddSession();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/AspNetCore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection(); //==> raison de s�curit� pour rediriger les requettes vers le protocole https
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
