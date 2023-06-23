using ContactManager.BusinessLogic;
using ContactManager.BusinessLogic.Data;
using ContactManager.Web.Models;
using ContactManager.Web.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Ignore SSL certificate validation
SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConection"));
sqlConnectionStringBuilder.TrustServerCertificate = true;
var connectionString = sqlConnectionStringBuilder.ConnectionString;
//var connectionString = builder.Configuration.GetConnectionString("DefaultConection");

builder.Services.AddDbContext<ContactManagerContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddHttpClient("BingMaps", c =>
{
    c.BaseAddress = new Uri("https://dev.virtualearth.net/");
    c.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<BingMapsService, BingMapsService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<ContactManagerContext>();
    var numberOfContacts = builder.Configuration.GetSection("NumberOfContacts").Value;
    DataGenerator.NumberOfContacts = (numberOfContacts!=null)? int.Parse(numberOfContacts) : 5;
    DataGenerator.InitData();
    dbContext.Database.EnsureCreated();
}


app.Run();
