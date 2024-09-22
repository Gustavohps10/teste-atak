using teste_atak.Infra.Data.Seed;
using teste_atak.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Database
builder.Services.AddInfrastructure(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UsePathBase("/api");
app.UseRouting();

if (args.Length > 0 && args[0] == "seed")
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    var dataGenerator = services.GetRequiredService<BogusDataGenerator>();
    await dataGenerator.SeedCustomers(); 

    return;
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
