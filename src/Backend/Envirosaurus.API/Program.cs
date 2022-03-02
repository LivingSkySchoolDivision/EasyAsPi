using Envirosaurus;
using Envirosaurus.API.Services;
using LSSD.MongoDB;

var builder = WebApplication.CreateBuilder(args);

/*
Using dotnet user-secrets in development
dotnet user-secrets init --project /path/to/project
dotnet user-secrets set "ConnectionStrings:MyConnectionString" "Value"
*/

builder.Configuration.AddEnvironmentVariables(); // I don't think this is neccesary anymore but it's here anyway

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<SensorService>();
builder.Services.AddSingleton<SensorReadingService>();
builder.Services.AddSingleton<MongoDbConnection>(x => new MongoDbConnection(builder.Configuration["ConnectionStrings:InternalDatabase"]));
builder.Services.AddSingleton<IRepository<Sensor>, MongoRepository<Sensor>>();
builder.Services.AddSingleton<IRepository<SensorReading>, MongoRepository<SensorReading>>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
