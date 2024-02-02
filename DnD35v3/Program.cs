using DnD35v3.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using MudBlazor.Services;
using Auth0.AspNetCore.Authentication;
using DnD35v3.Modules;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddScoped<UserAccount>();
builder.Services.AddScoped<FetchUserData>();
builder.Services.AddScoped<UserToken>();
builder.Services.AddScoped<ThemeStateService>();
builder.Services.AddScoped<SaveThemeState>();
builder.Services.AddScoped<Validation>();
builder.Services.AddSingleton<UserList>();
builder.Services.AddSingleton<DungeonGroups>();
builder.Services.AddSingleton<DungeonTracker>();
builder.Services.AddSingleton<GroupHandler>();
builder.Services.AddSingleton<FetchUserList>();

builder.Services
    .AddAuth0WebAppAuthentication(options =>
    {
        options.Domain = builder.Configuration["Auth0:Domain"]!;
        options.ClientId = builder.Configuration["Auth0:ClientId"]!;
    });

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear(); // Its loopback by default, so we need to clear it if your server serves directly to the Internet.
    options.KnownProxies.Clear();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseForwardedHeaders();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

var groupHandler = app.Services.GetRequiredService<GroupHandler>();
await groupHandler.FetchGroups();
await groupHandler.FetchTracker();

var userHandler = app.Services.GetRequiredService<FetchUserList>();
await userHandler.FetchUsers();

app.Run();