
namespace Campoverde.QMS.Services;

public class QuoteService(CampoverdeDbContext dbContext,
    IUserService userService,
    ICustomerService customerService,
    IMailService mailService)
    : IQuoteService
{
    private readonly CampoverdeDbContext _dbContext = dbContext;
    private readonly IUserService _userService = userService;
    private readonly ICustomerService _customerService = customerService;
    private readonly IMailService _mailService = mailService;
    public async Task<string> CreateQuoteAsync(Quote quote)
    {
        try
        {
            if (quote.IsUserAccountNeeded == "yes")
            {
                User user = new()
                {
                    Email = quote.Customer.Email,
                    Phone = quote.Customer.Phone,
                    Password = quote.Customer.Email,
                    RoleId = _dbContext.Role.Where(r => r.Name.ToLower() == "user").FirstOrDefault().Id
                };
                await _userService.CreateUserAsync(user);
            }
            quote.CustomerId = await _customerService.CreateCustomerAsync(quote.Customer);

            var dbitem = await _dbContext.Quote.FindAsync(quote.Id);
            if (dbitem == null)
            {
                quote.IsActive = true;
                quote.IsDeleted = false;
                quote.LastUpdatedByUser = DateTime.UtcNow;
                quote.LastUpdatedTime = DateTime.UtcNow;
                await _dbContext.Quote.AddAsync(quote);
            }
            else
            {
                var quoteUpdate = new Quote
                {
                    Id = quote.Id,
                    IsActive = quote.IsActive,
                    IsDeleted = quote.IsDeleted,
                    SpanishAddress = quote.SpanishAddress,
                    CustomerId = quote.CustomerId,
                    EndDate = quote.EndDate,
                    LastUpdatedByUser = DateTime.UtcNow,
                    LastUpdatedTime = DateTime.UtcNow,
                    PassengerCount = quote.PassengerCount,
                    QuotePrice = quote.QuotePrice,
                    SpecialRequet = quote.SpecialRequet,
                    StartDate = quote.StartDate,
                    Status = quote.Status,
                    VehicleId = quote.VehicleId,
                    VehicleSize = quote.VehicleSize,
                    VehicleType = quote.VehicleType
                };
                _dbContext.Quote.Update(quoteUpdate);
            }
            await _dbContext.SaveChangesAsync();

            _mailService.SendMail("sifa1258@gmail.com", "", "Quote creation", "Test quote create");
            return "Success";
        }
        catch (Exception ex)
        {
            return $"Quote creation failed with message {ex.Message} inner exception {ex.InnerException?.Message}";
        }
    }
}
