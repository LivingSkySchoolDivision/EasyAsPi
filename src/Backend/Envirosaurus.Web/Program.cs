using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Envirosaurus;
using Envirosaurus.Web.Services;
using LSSD.MongoDB;

var builder = WebApplication.CreateBuilder(args);

// Set up the timezone that the UI should be in
if (!string.IsNullOrEmpty(builder.Configuration["Settings:Timezone"])) {
    GlobalSettings.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(builder.Configuration["Settings:Timezone"]);
}

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<SensorService>();
builder.Services.AddSingleton<SensorReadingService>();
builder.Services.AddSingleton<FacilityMapService>();
builder.Services.AddSingleton<MongoDbConnection>(x => new MongoDbConnection(builder.Configuration["ConnectionStrings:InternalDatabase"]));
builder.Services.AddSingleton<IRepository<Sensor>, MongoRepository<Sensor>>();
builder.Services.AddSingleton<IRepository<SensorReading>, MongoRepository<SensorReading>>();
builder.Services.AddSingleton<IRepository<FacilityMap>, MongoRepository<FacilityMap>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
