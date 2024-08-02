using AirlineManagement.Business.Mappings;
using AirlineManagement.MvcUI.Services;
using AirlineManagement.Presentation;
using Autofac.Extensions.DependencyInjection;

namespace AirlineManagement.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Add services to the container.
            builder.Services.AddHttpClient<FlightApiService>(opt =>
            {
                opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
            });
            builder.Services.AddHttpClient<AirlineApiService>(opt =>
            {
                opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
            });
            builder.Services.AddHttpClient<ReservationApiService>(opt =>
            {
                opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
            });
            builder.Services.AddHttpClient<CheckInApiService>(opt =>
            {
                opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
            });
            builder.Services.AddControllersWithViews();
            builder.Services.AddServiceRegistration();
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}