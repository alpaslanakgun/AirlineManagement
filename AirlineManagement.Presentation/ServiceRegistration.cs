using AirlineManagement.Business.Contracts;
using AirlineManagement.Business.Services;
using AirlineManagement.Data.Configurations.DatabaseConfigurations;
using AirlineManagement.Data.Context;
using AirlineManagement.Data.Contracts;
using AirlineManagement.Data.Repositories;
using Microsoft.EntityFrameworkCore;


namespace AirlineManagement.Presentation
{
    public static class ServiceRegistration
    {
        public static void AddServiceRegistration(this IServiceCollection services)
        {
            services.AddDbContext<AirlineContext>(options => options.UseSqlServer(Configuration.ConnectionString));
    
            // Repository registrations
            services.AddScoped<IAirlineRepository, AirlineRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IPassengerRepository, PassengerRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<ICheckInRepository, CheckInRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Service registrations
            services.AddScoped<IAirlineService, AirlineService>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IPassengerService, PassengerService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<ICheckInService, CheckInService>();

           
        }
    }
}

