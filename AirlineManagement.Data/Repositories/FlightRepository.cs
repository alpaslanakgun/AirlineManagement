using AirlineManagement.Data.Context;
using AirlineManagement.Data.Contracts;
using AirlineManagement.Domain.Entities;
using AirlineManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace AirlineManagement.Data.Repositories
{
    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        private readonly AirlineContext _dbContext;

        public FlightRepository(AirlineContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Flight>> SearchFlightsAsync(string departureAirport, string arrivalAirport, DateTime departureDate)
        {
            return await _dbContext.Flights
                .Where(f => f.DepartureAirport == departureAirport &&
                            f.ArrivalAirport == arrivalAirport &&
                            f.DepartureTime.Date == departureDate.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetFlightsWithDetailsAsync()
        {
            return await _dbContext.Flights
                .Include(f => f.DepartureAirportNavigation)
                .Include(f => f.ArrivalAirportNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Flight>> SearchFlightsAsync(string departureAirport, string arrivalAirport, DateTime? departureDate, string status)
        {
            var query = _dbContext.Flights.AsQueryable();

            if (!string.IsNullOrEmpty(departureAirport))
            {
                query = query.Where(f => f.DepartureAirport == departureAirport);
            }

            if (!string.IsNullOrEmpty(arrivalAirport))
            {
                query = query.Where(f => f.ArrivalAirport == arrivalAirport);
            }

            if (departureDate.HasValue)
            {
                query = query.Where(f => f.DepartureTime.Date == departureDate.Value.Date);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(f => f.Status == status);
            }

            return await query.AsNoTracking().ToListAsync();
        }
    }
}
