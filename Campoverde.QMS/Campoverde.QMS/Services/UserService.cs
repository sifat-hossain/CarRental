namespace Campoverde.QMS.Services;

public class UserService(CampoverdeDbContext dbContext) : IUserService
{
    private readonly CampoverdeDbContext _dbContext = dbContext;
    public async Task<string> CreateUserAsync(User user)
    {
        try
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            else
            {
                var dbitem = await _dbContext.User.Where(e => e.Email == user.Email).FirstOrDefaultAsync();
                if (dbitem == null)
                {
                    user.IsDeleted = false;
                    user.IsActive = true;
                    user.Password = PasswordEncryption.HashPassword(user.Password);
                    await _dbContext.User.AddAsync(user);
                    return "success";
                }
                else
                {
                    dbitem.Password = PasswordEncryption.HashPassword(user.Password);
                    _dbContext.Update(dbitem);
                    return "success";
                }
                return "User already exist";
            }
        }
        catch (Exception ex)
        {
            return $"Failed with messae {ex.Message} inner exception {ex.InnerException?.Message}";
        }
    }

}
