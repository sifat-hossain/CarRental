using Microsoft.AspNetCore.Mvc.Rendering;

namespace Campoverde.QMS.Controllers;

public class VehiclePriceController(CampoverdeDbContext context) : Controller
{
    private readonly CampoverdeDbContext _context = context;

    // GET: VehiclePrice
    public async Task<IActionResult> Index()
    {
        var campoverdeDbContext = _context.VehiclePrice.Include(v => v.Season).Include(v => v.Vehicle);
        return View(await campoverdeDbContext.ToListAsync());
    }

    // GET: VehiclePrice/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vehiclePrice = await _context.VehiclePrice
            .Include(v => v.Season)
            .Include(v => v.Vehicle)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (vehiclePrice == null)
        {
            return NotFound();
        }

        return View(vehiclePrice);
    }

    // GET: VehiclePrice/Create
    public IActionResult Create()
    {
        ViewData["SeasonId"] = new SelectList(_context.Set<Season>(), "Id", "Name");
        ViewData["VehicleId"] = new SelectList(_context.Vehicle, "Id", "Model");
        return View();
    }

    // POST: VehiclePrice/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("VehicleId,SeasonId,Price,Id,IsDeleted,IsActive")] VehiclePrice vehiclePrice)
    {
        if (ModelState.IsValid)
        {
            vehiclePrice.Id = Guid.NewGuid();
            _context.Add(vehiclePrice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["SeasonId"] = new SelectList(_context.Set<Season>(), "Id", "Name", vehiclePrice.SeasonId);
        ViewData["VehicleId"] = new SelectList(_context.Vehicle, "Id", "Model", vehiclePrice.VehicleId);
        return View(vehiclePrice);
    }

    // GET: VehiclePrice/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vehiclePrice = await _context.VehiclePrice.FindAsync(id);
        if (vehiclePrice == null)
        {
            return NotFound();
        }
        ViewData["SeasonId"] = new SelectList(_context.Set<Season>(), "Id", "Name", vehiclePrice.SeasonId);
        ViewData["VehicleId"] = new SelectList(_context.Vehicle, "Id", "Model", vehiclePrice.VehicleId);
        return View(vehiclePrice);
    }

    // POST: VehiclePrice/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("VehicleId,SeasonId,Price,Id,IsDeleted,IsActive")] VehiclePrice vehiclePrice)
    {
        if (id != vehiclePrice.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(vehiclePrice);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiclePriceExists(vehiclePrice.Id))
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
        ViewData["SeasonId"] = new SelectList(_context.Set<Season>(), "Id", "Name", vehiclePrice.SeasonId);
        ViewData["VehicleId"] = new SelectList(_context.Vehicle, "Id", "Model", vehiclePrice.VehicleId);
        return View(vehiclePrice);
    }

    // GET: VehiclePrice/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vehiclePrice = await _context.VehiclePrice
            .Include(v => v.Season)
            .Include(v => v.Vehicle)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (vehiclePrice == null)
        {
            return NotFound();
        }

        return View(vehiclePrice);
    }

    // POST: VehiclePrice/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var vehiclePrice = await _context.VehiclePrice.FindAsync(id);
        if (vehiclePrice != null)
        {
            _context.VehiclePrice.Remove(vehiclePrice);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VehiclePriceExists(Guid id)
    {
        return _context.VehiclePrice.Any(e => e.Id == id);
    }
}
