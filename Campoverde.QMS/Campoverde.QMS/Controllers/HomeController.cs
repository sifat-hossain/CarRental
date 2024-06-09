using Campoverde.QMS.ViewModel;

namespace Campoverde.QMS.Controllers;

public class HomeController(ILogger<HomeController> logger,
    CampoverdeDbContext campoverdeDbContext,
    IQuoteService quoteService) : Controller
{

    private readonly ILogger<HomeController> _logger = logger;
    private readonly CampoverdeDbContext _campoverdeDbContext = campoverdeDbContext;
    private readonly IQuoteService _quoteService = quoteService;

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Index(QuoteViewModel quoteViewModel)
    {
        var quote = new Quote
        {
            PickupLocation = quoteViewModel.PickupLocation,
            DropLocation = quoteViewModel.DropLocation,
            IsActive = true,
            IsDeleted = false,
            SpanishAddress = "N/A",
            IsUserAccountNeeded = "yes",
            Customer = new Customer
            {
                FirstName = quoteViewModel.FirstName,
                LastName = quoteViewModel.LastName,
                Email = quoteViewModel.Email,
                Nationality = quoteViewModel.Nationality,
                Age = quoteViewModel.Age,
                Gender = quoteViewModel.Gender,
                Phone = quoteViewModel.Phone
            },
            SpecialRequet = "N/A",
            EndDate = quoteViewModel.EndDate,
            LastUpdatedByUser = DateTime.UtcNow,
            LastUpdatedTime = DateTime.UtcNow,
            PassengerCount = PassengerCountEnum.FourPerson,
            StartDate = quoteViewModel.StartDate,
            Status = QuoteStatusEnum.New,
            VehicleId = quoteViewModel.VehicleId,
            QuotePrice = quoteViewModel.Price
        };

        await _quoteService.CreateQuoteAsync(quote);

        return RedirectToAction("Confirmation", "Quote");
    }
}
