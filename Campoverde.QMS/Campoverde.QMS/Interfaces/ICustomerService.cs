namespace Campoverde.QMS.Interfaces;

public interface ICustomerService
{
    Task<int> CreateCustomerAsync(Customer customer);
}
