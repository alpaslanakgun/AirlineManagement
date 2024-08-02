using AirlineManagement.Business.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AirlineManagement.UI.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string origin, string destination, DateTime departureDate)
        {
            var result = await _flightService.SearchFlightsAsync(origin, destination, departureDate);
            if (result.Success)
            {
                return View("SearchResults", result.Data);
            }
            ViewBag.ErrorMessage = result.Message;
            return View("Index");
        }
    }
}
