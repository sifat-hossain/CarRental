namespace Campoverde.QMS.Interfaces;

public interface IUserService
{
    Task<string> CreateUserAsync(User user);
}
