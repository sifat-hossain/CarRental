namespace Campoverde.QMS.Services;

public class CustomerService(CampoverdeDbContext dbContext) : ICustomerService
{
    private readonly CampoverdeDbContext _dbContext = dbContext;
    public async Task<int> CreateCustomerAsync(Customer customer)
    {
        var dbitem = await _dbContext.Customer
            .Where(c => c.Email == customer.Email)
            .FirstOrDefaultAsync();
        if (dbitem == null && customer != null && customer.Id <= 0)
        {
            customer.IsActive = true;
            customer.IsDeleted = false;
            customer.SpanishAddress = "N/a";

            await _dbContext.AddAsync(customer);
            _dbContext.SaveChanges();

            return customer.Id;
        }
        return dbitem.Id;
    }
}
