using Microsoft.EntityFrameworkCore;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.DTO;
using ProvidersBase.Model.Mappers;
using ProvidersBase.Model.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Add the DB context to the container and include the connection string from the appsettings.json file. If the connection string is not found, it will return an exception 
builder.Services.AddDbContext<ProvidersContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Default connection not found.");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
// This middleware helps to detect and diagnose errors with Entity Framework Core migrations.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Add controers for restAPI
builder.Services.AddControllers();

//Add mapping interfaces in DI
builder.Services.AddScoped<IMapper<ProviderCompany, ProviderCompanyDTO>, ProviderCompanyMapper>();
builder.Services.AddScoped<IMapper<ProviderProduct, ProviderProductDTO>, ProviderProductMapper>();
builder.Services.AddScoped<IMapper<ProviderUser, ProviderUserDTO>, ProviderUserMapper>();


var app = builder.Build();


// This middleware helps to detect and diagnose errors with Entity Framework Core migrations.
app.UseDeveloperExceptionPage();
app.UseMigrationsEndPoint();

//This code creates a new scope for the application services, gets the required ProvidersContext service,
//ensures the database is created, and initializes the database using the DbInitializer class.
//The DbInitializer class contains code to populate the database with initial data.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ProvidersContext>();
    context.Database.EnsureCreated();
    DBInitializer.GenerateTestData(context);
}


app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseCors("CorsPolicy");


app.Run();
