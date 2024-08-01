using AirlineManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Data.Contracts
{
    public interface IFlightRepository : IGenericRepository<Flight>
    {
        Task<IEnumerable<Flight>> SearchFlightsAsync(string departureAirport, string arrivalAirport, DateTime departureDate);
        Task<IEnumerable<Flight>> GetFlightsWithDetailsAsync();
        Task<IEnumerable<Flight>> SearchFlightsAsync(string departureAirport, string arrivalAirport, DateTime? departureDate, string status);
    }
}
