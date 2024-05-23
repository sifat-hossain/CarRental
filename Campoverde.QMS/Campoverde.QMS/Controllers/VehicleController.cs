using Microsoft.AspNetCore.Authorization;

namespace Campoverde.QMS.Controllers;

[Authorize(Roles = "Admin")]
public class VehicleController(CampoverdeDbContext context) : Controller
{
    private readonly CampoverdeDbContext _context = context;

    // GET: Vehicle
    public async Task<IActionResult> Index()
    {
        return View(await _context.Vehicle.OrderByDescending(x => x.Id).ToListAsync());
    }

    // GET: Vehicle/Details/5
    public async Task<IActionResult> Details(int? id)
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

    // GET: Vehicle/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Vehicle/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Model,Price,Id,IsDeleted,IsActive")] Vehicle vehicle)
    {
        if (ModelState.IsValid)
        {
            _context.Add(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(vehicle);
    }

    // GET: Vehicle/Edit/5
    public async Task<IActionResult> Edit(int? id)
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

    // POST: Vehicle/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Model,Price,Id,IsDeleted,IsActive")] Vehicle vehicle)
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

    // GET: Vehicle/Delete/5
    public async Task<IActionResult> Delete(int? id)
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

    // POST: Vehicle/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var vehicle = await _context.Vehicle.FindAsync(id);
        if (vehicle != null)
        {
            _context.Vehicle.Remove(vehicle);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VehicleExists(int id)
    {
        return _context.Vehicle.Any(e => e.Id == id);
    }
}
