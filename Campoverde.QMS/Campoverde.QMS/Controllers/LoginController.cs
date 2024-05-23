using Campoverde.QMS.Common;
using Campoverde.QMS.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Campoverde.QMS.Controllers;

public class LoginController(CampoverdeDbContext dbContext) : Controller
{
    private readonly CampoverdeDbContext _dbContext = dbContext;
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _dbContext.User
                .Include(r => r.Role)
                .SingleOrDefaultAsync(u => u.Email == model.Email);


            if (user != null && PasswordEncryption.VerifyPassword(model.Password, user.Password)) // Ideally, use a hashed password comparison
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.Email),
                    new(ClaimTypes.Role, user.Role.Name)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Quote");
                }
                else
                {
                    return RedirectToAction("Create", "Quote");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
