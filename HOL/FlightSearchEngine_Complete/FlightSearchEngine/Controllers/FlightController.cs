using FlightSearchEngine.Data;
using FlightSearchEngine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightSearchEngine.Controllers
{
    public class FlightController : Controller
    {
        private readonly DatabaseHelper _db;

        public FlightController(IConfiguration configuration)
        {
            _db = new DatabaseHelper(configuration);
        }

        // -------------------------------------------------------
        // Helper: populate dropdowns on the SearchViewModel
        // -------------------------------------------------------
        private async Task PopulateDropdownsAsync(SearchViewModel model)
        {
            var sources      = await _db.GetSourcesAsync();
            var destinations = await _db.GetDestinationsAsync();

            model.SourceList      = new SelectList(sources);
            model.DestinationList = new SelectList(destinations);
        }

        // -------------------------------------------------------
        // GET: /Flight/Index
        // -------------------------------------------------------
        public async Task<IActionResult> Index()
        {
            var model = new SearchViewModel();
            await PopulateDropdownsAsync(model);
            return View(model);
        }

        // -------------------------------------------------------
        // POST: /Flight/SearchFlights
        // -------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchFlights(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(model);
                return View("Index", model);
            }

            if (model.Source == model.Destination)
            {
                ModelState.AddModelError(string.Empty, "Source and Destination cannot be the same.");
                await PopulateDropdownsAsync(model);
                return View("Index", model);
            }

            try
            {
                var results = await _db.SearchFlightsAsync(model.Source, model.Destination, model.NumberOfPersons);
                ViewBag.SearchType     = "FlightOnly";
                ViewBag.Source         = model.Source;
                ViewBag.Destination    = model.Destination;
                ViewBag.Persons        = model.NumberOfPersons;
                return View("Results", results);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                await PopulateDropdownsAsync(model);
                return View("Index", model);
            }
        }

        // -------------------------------------------------------
        // POST: /Flight/SearchFlightsWithHotels
        // -------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchFlightsWithHotels(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(model);
                return View("Index", model);
            }

            if (model.Source == model.Destination)
            {
                ModelState.AddModelError(string.Empty, "Source and Destination cannot be the same.");
                await PopulateDropdownsAsync(model);
                return View("Index", model);
            }

            try
            {
                var results = await _db.SearchFlightsWithHotelsAsync(model.Source, model.Destination, model.NumberOfPersons);
                ViewBag.SearchType     = "FlightAndHotel";
                ViewBag.Source         = model.Source;
                ViewBag.Destination    = model.Destination;
                ViewBag.Persons        = model.NumberOfPersons;
                return View("Results", results);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                await PopulateDropdownsAsync(model);
                return View("Index", model);
            }
        }
    }
}
