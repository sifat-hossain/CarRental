
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.LogoutPath = "/Login/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5); // Set session timeout to 30 minutes
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"))
    .AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));

// Introduce session
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true; // Make the session cookie HTTP-only
    options.Cookie.IsEssential = true; // Make the session cookie essential
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CampoverdeDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("campoverdeDbConnection")));

// Depency inject
builder.Services.AddScoped<IVehicle, VehicleService>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IQuoteService, QuoteService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
