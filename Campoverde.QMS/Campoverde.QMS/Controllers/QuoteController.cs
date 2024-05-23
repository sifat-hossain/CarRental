﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Campoverde.QMS.Controllers
{
    public class QuoteController(CampoverdeDbContext context, IMailService mailService, IQuoteService quoteService) : Controller
    {
        private readonly CampoverdeDbContext _context = context;
        private readonly IQuoteService _quoteService = quoteService;

        // GET: Quotes
        public async Task<IActionResult> Index()
        {
            var campoverdeDbContext = _context.Quote.OrderByDescending(x => x.Id).Include(q => q.Customer).Include(q => q.Vehicle);
            return View(await campoverdeDbContext.ToListAsync());
        }

        // GET: Quotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quote = await _context.Quote
                .Include(q => q.Customer)
                .Include(q => q.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quote == null)
            {
                return NotFound();
            }

            return View(quote);
        }

        // GET: Quotes/Create
        public IActionResult Create()
        {

            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "Id", "Model");
            return View();
        }

        // POST: Quotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId,Customer,VehicleSize,VehicleType,PassengerCount,StartDate,EndDate,SpanishAddress,SpecialRequet,QuotePrice,LastUpdatedTime,LastUpdatedByUser,Status,Id,IsDeleted,IsActive,IsUserAccountNeeded")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                await _quoteService.CreateQuoteAsync(quote);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "FirstName", quote.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "Id", "Model", quote.VehicleId);
            return View(quote);
        }

        // GET: Quotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quote = await _context.Quote.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "FirstName", quote.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "Id", "Model", quote.VehicleId);
            return View(quote);
        }

        // POST: Quotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleId,CustomerId,VehicleSize,VehicleType,PassengerCount,StartDate,EndDate,SpanishAddress,SpecialRequet,QuotePrice,LastUpdatedTime,LastUpdatedByUser,Status,Id,IsDeleted,IsActive")] Quote quote)
        {
            if (id != quote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuoteExists(quote.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "FirstName", quote.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "Id", "Model", quote.VehicleId);
            return View(quote);
        }

        // GET: Quotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quote = await _context.Quote
                .Include(q => q.Customer)
                .Include(q => q.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quote == null)
            {
                return NotFound();
            }

            return View(quote);
        }

        // POST: Quotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quote = await _context.Quote.FindAsync(id);
            if (quote != null)
            {
                _context.Quote.Remove(quote);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuoteExists(int id)
        {
            return _context.Quote.Any(e => e.Id == id);
        }

        [HttpGet]
        public JsonResult GetPrice(int vehicleModelId)
        {

            var vehicleModel = _context.Vehicle.FirstOrDefault(vm => vm.Id == vehicleModelId);

            if (vehicleModel != null)
            {
                return Json(vehicleModel.Price);
            }

            return Json(null);
        }

        [HttpGet]
        public JsonResult GetModel(int vehicleModelId)
        {
            var vehicleModel = _context.Vehicle.FirstOrDefault(vm => vm.Id == vehicleModelId);

            if (vehicleModel != null)
            {
                return Json(vehicleModel.Model);
            }
            return Json(null);
        }

        [HttpPost]
        public JsonResult ChangeStatus(int quoteId, QuoteStatusEnum status)
        {
            var quote = _context.Quote.Where(q => q.Id == quoteId).FirstOrDefault();
            if (quote != null)
            {
                quote.Status = status;
                _context.Update(quote);
                _context.SaveChanges();
                return Json(true);
            }
            return Json(null);
        }
    }
}
