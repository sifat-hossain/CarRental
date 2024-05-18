using Campoverde.QMS.Models;

namespace Campoverde.QMS.Controllers;

public class VehicleController(IVehicle vehicle) : Controller
{
    private readonly IVehicle _vehicle = vehicle;

    public async Task<IActionResult> Index()
    {
        var vehicles = await _vehicle.GetAllVehiclesAsync();
        return View(vehicles);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Vehicle vehicle)
    {
        var result = await _vehicle.CreateVehicleAsync(vehicle);
        return RedirectToAction("Index");
    }
}
