using Microsoft.EntityFrameworkCore;
using MVCLibraryManage.Models;
using MVCLibraryManage.Repository;
using MVCLibraryManage.Repository.Implement;
using MVCLibraryManage.Service;
using MVCLibraryManage.Service.Implements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://localhost:44344")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // Allow cookies/credentials
    });
});

// Configure Database Context
builder.Services.AddDbContext<LibraryDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Default");
    options.UseSqlServer(connectionString);
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});


// Register repositories and services
builder.Services.AddScoped<IBorrowerRepository, BorrowerRepository>();
builder.Services.AddScoped<ILibraryItemRepository, LibraryItemRepository>();
builder.Services.AddScoped<IBorrowingHistoryRepository, BorrowingHistoryRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IBorrowerService, BorrowerService>();
builder.Services.AddScoped<ILibraryItemService, LibraryItemService>();
builder.Services.AddScoped<IBorrowingHistoryService, BorrowingHistoryService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

// Apply CORS policy before session and other middlewares
app.UseCors("AllowSpecificOrigin");

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Referrer-Policy", "no-referrer-when-downgrade");
    await next();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Ensure session is used after routing and before authorization
app.UseRouting();
app.UseSession();  // Session middleware should be used after routing
app.UseAuthorization();

app.MapRazorPages();

// Map controllers and configure the default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
