namespace Campoverde.QMS.Interfaces;

public interface ICustomerService
{
    Task<Guid> CreateCustomerAsync(Customer customer);
}
