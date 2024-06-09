using Campoverde.QMS.ViewModel;

namespace Campoverde.QMS.Controllers;

public class VehicleController(CampoverdeDbContext context, IWebHostEnvironment hostEnvironment) : Controller
{
    private readonly CampoverdeDbContext _context = context;
    private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;

    // GET: Vehicles
    public async Task<IActionResult> Index()
    {
        return View(await _context.Vehicle.ToListAsync());
    }

    // GET: Vehicles/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vehicle = await _context.Vehicle
            .FirstOrDefaultAsync(m => m.Id == id);
        if (vehicle == null)
        {
            return NotFound();
        }

        return View(vehicle);
    }

    // GET: Vehicles/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Vehicles/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Model,VehicleSize,VehicleTypeEnum,PhotoUrl,Id,IsDeleted,IsActive,Image")] Vehicle vehicle)
    {
        if (ModelState.IsValid)
        {
            string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
            string uniqueFileName = vehicle.Model + "_" + vehicle.Image.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Ensure the directory exists
            Directory.CreateDirectory(uploadsFolder);

            // Copy the file to the target location
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await vehicle.Image.CopyToAsync(fileStream);
            }
            vehicle.PhotoUrl = "~/images/" + uniqueFileName;
            vehicle.Id = Guid.NewGuid();
            _context.Add(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(vehicle);
    }

    // GET: Vehicles/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vehicle = await _context.Vehicle.FindAsync(id);
        if (vehicle == null)
        {
            return NotFound();
        }
        return View(vehicle);
    }

    // POST: Vehicles/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Model,VehicleSize,VehicleTypeEnum,PhotoUrl,Id,IsDeleted,IsActive")] Vehicle vehicle)
    {
        if (id != vehicle.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(vehicle);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(vehicle.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(vehicle);
    }

    // GET: Vehicles/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vehicle = await _context.Vehicle
            .FirstOrDefaultAsync(m => m.Id == id);
        if (vehicle == null)
        {
            return NotFound();
        }

        return View(vehicle);
    }

    // POST: Vehicles/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var vehicle = await _context.Vehicle.FindAsync(id);
        if (vehicle != null)
        {
            _context.Vehicle.Remove(vehicle);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VehicleExists(Guid id)
    {
        return _context.Vehicle.Any(e => e.Id == id);
    }

    [HttpPost]
    public JsonResult GetAvailableVehicles(DateTime startDate, DateTime endDate)
    {
        List<int> lowSeason = [2, 11];
        List<int> midSeason = [1, 3, 4, 5, 6, 10, 12];
        List<int> highSeason = [7, 8, 9];

        var month = startDate.Month;

        double totalHours = (endDate - startDate).TotalHours;

        List<VehiclePrice> availableVehicles = [];

        if (lowSeason.Contains(month))
        {
            availableVehicles = [.. _context.VehiclePrice
               .Include(v => v.Vehicle)
               .Include(s => s.Season)
               .Where(vp => vp.Season.Name == "Low Season")];
        }
        if (midSeason.Contains(month))
        {
            availableVehicles = [.. _context.VehiclePrice
               .Include(v => v.Vehicle)
               .Include(s => s.Season)
               .Where(vp => vp.Season.Name == "Mid Season")];
        }
        if (highSeason.Contains(month))
        {
            availableVehicles = [.. _context.VehiclePrice
               .Include(v => v.Vehicle)
               .Include(s => s.Season)
               .Where(vp => vp.Season.Name == "High Season")];
        }

        var vehicleViewModel = availableVehicles.Select(x => new VehicleViewModel
        {
            Id = x.Vehicle.Id,
            Model = x.Vehicle.Model,
            VehicleType = Enum.GetName(typeof(VehicleTypeEnum), x.Vehicle.VehicleTypeEnum),
            VehicleSize = Enum.GetName(typeof(VehicleTypeEnum), x.Vehicle.VehicleSize),
            PhotoUrl = x.Vehicle.PhotoUrl,
            Price = x.Price * (Convert.ToDecimal(totalHours))

        }).ToList();

        return Json(vehicleViewModel);
    }
}
