using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using TransportScale.Core.Configuration;
using TransportScale.Data;
using TransportScale.Data.Configuration;
using TransportScale.Server;
using TransportScale.Server.HealthCheck;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TransportDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.MakeRepositoryDependencies();
builder.Services.MakeServiceDependencies();
builder.Services.AddAutoMapper(typeof(MapperConfiguration));

builder.Services.AddHealthChecks()
                .AddCheck<LivenessCheck>("liveness", tags: new[] { "liveness" })
                .AddCheck<ReadinessCheck>("readiness", tags: new[] { "readiness" });

builder.Services.AddMemoryCache();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health/liveness", new HealthCheckOptions { Predicate = x => x.Name == "liveness" });
    endpoints.MapHealthChecks("/health/readiness", new HealthCheckOptions { Predicate = x => x.Name == "readiness" });
    endpoints.MapRazorPages();
    endpoints.MapControllers();
    endpoints.MapFallbackToFile("index.html");
});

await SeedData.PopulateDb(builder);

app.Run();
