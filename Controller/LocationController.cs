using CountryStateCityApp.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class LocationController : Controller
{
    private readonly LocationRepository _repository;

    // Use dependency injection to get an instance of LocationRepository
    public LocationController(LocationRepository repository)
    {
        _repository = repository;
    }

    // View Countries
    public IActionResult Index()
    {
        var countries = _repository.GetCountries();
        return View(countries);
    }

    // View States by Country
    public IActionResult States(int countryId)
    {
        var states = _repository.GetStatesByCountryId(countryId);
        return View(states);
    }

    // View Cities by State
    public IActionResult Cities(int stateId)
    {
        var cities = _repository.GetCitiesByStateId(stateId);
        return View(cities);
    }

    // Add Country
    public IActionResult AddCountry()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddCountry(Country country)
    {
        if (ModelState.IsValid)
        {
            _repository.AddCountry(country);
            return RedirectToAction("Index");
        }
        return View(country);
    }

    // Edit Country
    public IActionResult EditCountry(int id)
    {
        var country = _repository.GetCountries().FirstOrDefault(c => c.CountryId == id);
        if (country == null) return NotFound();
        return View(country);
    }

    [HttpPost]
    public IActionResult EditCountry(Country country)
    {
        if (ModelState.IsValid)
        {
            _repository.UpdateCountry(country);
            return RedirectToAction("Index");
        }
        return View(country);
    }

    // Delete Country
    public IActionResult DeleteCountry(int id)
    {
        _repository.DeleteCountry(id);
        return RedirectToAction("Index");
    }
}
