
namespace Campoverde.QMS.Services;

public class CustomerService(CampoverdeDbContext dbContext) : ICustomerService
{
    private readonly CampoverdeDbContext _dbContext = dbContext;
    public async Task<int> CreateCustomerAsync(Customer customer)
    {
        if (customer != null && customer.Id <= 0)
        {
            customer.IsActive = true;
            customer.IsDeleted = false;
            customer.SpanishAddress = "N/a";

            await _dbContext.AddAsync(customer);
            _dbContext.SaveChanges();

            return customer.Id;
        }
        return 0;
    }
}
