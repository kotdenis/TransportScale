using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TransportScale.Client;
using TransportScale.Client.Services.Implementation;
using TransportScale.Client.Services.Interfaces;
using TransportScale.Dto;
using TransportScale.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ITransportService, TransportService>();
builder.Services.AddScoped<IJournalService, JournalService>();
builder.Services.AddSingleton<ChartState>();
builder.Services.AddSingleton<JournalState>();
builder.Services.AddSingleton<TransportState>();
builder.Services.AddScoped<UpdateState>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
