using Radzen;
using CP.Server.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;
using CP.Server.Data;
using Microsoft.AspNetCore.Identity;
using CP.Server.Models;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveWebAssemblyComponents();
builder.Services.AddControllers();
builder.Services.AddRadzenComponents();
builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = "CPTheme";
    options.Duration = TimeSpan.FromDays(365);
});
builder.Services.AddHttpClient();
builder.Services.AddScoped<CP.Server.CarParkService>();
builder.Services.AddDbContext<CP.Server.Data.CarParkContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("CarParkConnection"));
});
builder.Services.AddControllers().AddOData(opt =>
{
    var oDataBuilderCarPark = new ODataConventionModelBuilder();
    oDataBuilderCarPark.EntitySet<CP.Server.Models.CarPark.Driver>("Drivers");
    oDataBuilderCarPark.EntitySet<CP.Server.Models.CarPark.TripRequest>("TripRequests");
    oDataBuilderCarPark.EntitySet<CP.Server.Models.CarPark.Trip>("Trips");
    oDataBuilderCarPark.EntitySet<CP.Server.Models.CarPark.User>("Users");
    oDataBuilderCarPark.EntitySet<CP.Server.Models.CarPark.Vehicle>("Vehicles");
    oDataBuilderCarPark.EntitySet<CP.Server.Models.CarPark.AspNetRoleClaim>("AspNetRoleClaims");
    oDataBuilderCarPark.EntitySet<CP.Server.Models.CarPark.AspNetRole>("AspNetRoles");
    oDataBuilderCarPark.EntitySet<CP.Server.Models.CarPark.AspNetUserClaim>("AspNetUserClaims");
    oDataBuilderCarPark.EntitySet<CP.Server.Models.CarPark.AspNetUserLogin>("AspNetUserLogins").EntityType.HasKey(entity => new { entity.LoginProvider, entity.ProviderKey });
    oDataBuilderCarPark.EntitySet<CP.Server.Models.CarPark.AspNetUserRole>("AspNetUserRoles").EntityType.HasKey(entity => new { entity.UserId, entity.RoleId });
    oDataBuilderCarPark.EntitySet<CP.Server.Models.CarPark.AspNetUser>("AspNetUsers");
    oDataBuilderCarPark.EntitySet<CP.Server.Models.CarPark.AspNetUserToken>("AspNetUserTokens").EntityType.HasKey(entity => new { entity.UserId, entity.LoginProvider, entity.Name });
    oDataBuilderCarPark.Function("VDrivers").Returns<CP.Server.Models.CarPark.VDriver>();
    oDataBuilderCarPark.Function("VTripRequests").Returns<CP.Server.Models.CarPark.VTripRequest>();
    oDataBuilderCarPark.Function("VTrips").Returns<CP.Server.Models.CarPark.VTrip>();
    oDataBuilderCarPark.Function("VUsers").Returns<CP.Server.Models.CarPark.VUser>();
    oDataBuilderCarPark.Function("VVehicles").Returns<CP.Server.Models.CarPark.VVehicle>();
    opt.AddRouteComponents("odata/CarPark", oDataBuilderCarPark.GetEdmModel()).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
});
builder.Services.AddScoped<CP.Client.CarParkService>();
builder.Services.AddHttpClient("CP.Server").ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { UseCookies = false }).AddHeaderPropagation(o => o.Headers.Add("Cookie"));
builder.Services.AddHeaderPropagation(o => o.Headers.Add("Cookie"));
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddScoped<CP.Client.SecurityService>();
builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("CarParkConnection"));
});
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationIdentityDbContext>().AddDefaultTokenProviders();
builder.Services.AddControllers().AddOData(o =>
{
    var oDataBuilder = new ODataConventionModelBuilder();
    oDataBuilder.EntitySet<ApplicationUser>("ApplicationUsers");
    var usersType = oDataBuilder.StructuralTypes.First(x => x.ClrType == typeof(ApplicationUser));
    usersType.AddProperty(typeof(ApplicationUser).GetProperty(nameof(ApplicationUser.Password)));
    usersType.AddProperty(typeof(ApplicationUser).GetProperty(nameof(ApplicationUser.ConfirmPassword)));
    oDataBuilder.EntitySet<ApplicationRole>("ApplicationRoles");
    o.AddRouteComponents("odata/Identity", oDataBuilder.GetEdmModel()).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
});
builder.Services.AddScoped<AuthenticationStateProvider, CP.Client.ApplicationAuthenticationStateProvider>();
var app = builder.Build();
var forwardingOptions = new ForwardedHeadersOptions()
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
};
forwardingOptions.KnownIPNetworks.Clear();
forwardingOptions.KnownProxies.Clear();
app.UseForwardedHeaders(forwardingOptions);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found");
app.UseHttpsRedirection();
app.MapControllers();
app.UseHeaderPropagation();
app.MapStaticAssets();


var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
if (dbContext.Database.CanConnect())
{
    Console.WriteLine("База данных доступна");
}
else
{
    Console.WriteLine("Нет подключения к базе данных");
}

app.UseAuthorization();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveWebAssemblyRenderMode().AddAdditionalAssemblies(typeof(CP.Client._Imports).Assembly);
//app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>().Database.Migrate();
app.Run();