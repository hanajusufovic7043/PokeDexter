using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using PokeDexter.Components;
using PokeDexter.Components.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Configure authentication services
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
//})
//.AddCookie(options =>
//{
//    options.LoginPath = "/signin-google";
//    options.LogoutPath = "/logout";
//    options.Events.OnRedirectToLogin = context =>
//    {
//        context.Response.Redirect("/signin-google");
//        return System.Threading.Tasks.Task.CompletedTask;
//    };
//})
//.AddGoogle(options =>
//{
//    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
//    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
//    options.CallbackPath = "/signin-google"; 
//});

//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build(); // Enforce authentication for all requests
//});

builder.Services.AddHttpClient<IPokemonService, PokemonService>();
builder.Services.AddHttpClient();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddControllers();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowAllOrigins");

// Add Authentication and Authorization middleware
//app.UseAuthentication();
//app.UseAuthorization();

app.UseAntiforgery();

app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

//app.MapGet("/logout", async context =>
//{
//    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//    await context.SignOutAsync(GoogleDefaults.AuthenticationScheme);

//    // Clear the Google authentication session
//    var googleLogoutUrl = "https://accounts.google.com/Logout";
//    context.Response.Redirect(googleLogoutUrl);
//});

app.Run();
