using ActorModelExample.Domain.Services;
using ActorModelExample.DummyImpl.Services;
using ActorModelExample.WebApp.Util;
using ActorModelExample.DummyImpl.Hosting;

var builder = WebApplication.CreateBuilder(args);

var venue = DataGenerator.GenerateVenue();
builder.Services.AddDummy(venue);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton(venue);

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
