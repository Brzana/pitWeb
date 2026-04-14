using Microsoft.EntityFrameworkCore;
using pitWeb.api;
using pitWeb.Models;

using pitWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuring database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

// Add services to the container.
builder.Services.AddRazorPages();

// Add tax service.
builder.Services.AddScoped<ITaxService, TaxService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.RozliczenieEnpoints();

app.Run();
