using Campoverde.QMS.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(5) // Match the session timeout
                };

                var customer = await _dbContext.Customer.FirstOrDefaultAsync(c => c.Email == user.Email);
                if (customer != null)
                {
                    TempData["FullName"] = customer.FirstName + " " + customer.LastName;
                }
                else
                {
                    TempData["FullName"] = user.Email;
                }
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Quote");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(model);
    }


    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Authorize]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword([Bind("CurrentPassword", "NewPassword", "ConfirmPassword")] ChangePaqsswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _dbContext.User
                .FirstOrDefaultAsync(u => u.Email == User.Identity.Name); // Assuming Email is unique and used as an identifier

            if (user == null)
            {
                ViewBag.AlertMessage = "User not found.";
                return View(model);
            }

            if (!PasswordEncryption.VerifyPassword(model.CurrentPassword, user.Password))
            {
                ViewBag.AlertMessage = "Current password is incorrect.";
                return View(model);
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                ViewBag.AlertMessage = "New password and confirm password do not match.";
                return View(model);
            }

            // Update the user's password
            user.Password = PasswordEncryption.HashPassword(model.NewPassword); // Assuming you're hashing the password
            _dbContext.User.Update(user);
            await _dbContext.SaveChangesAsync();

            // Logout the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["AlertMessage"] = "Password changed successfully. Please log in with your new password.";

            // Redirect to a special action that displays the alert and handles the redirect
            return RedirectToAction("ShowChangePasswordAlert", "Login");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult ShowChangePasswordAlert()
    {
        if (TempData["AlertMessage"] != null)
        {
            ViewBag.AlertMessage = TempData["AlertMessage"];
        }
        return View();
    }
}
