using Ambe.Frontend.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Ambe.Frontend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IBitacoraService, BitacoraService>();
            builder.Services.AddScoped<IServicioLista, ServicioLista>();
            builder.Services.AddScoped<IServicioPersonas, ServicioPersonas>();
            builder.Services.AddScoped<IServicioViajes, ServicioViajes>();
            builder.Services.AddScoped<IServicioParametro, ServicioParametro>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {
              options.LoginPath = "/Login/IniciarSesion";
              options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
           });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=IniciarSesion}/{id?}");

            app.Run();
        }
    }
}
