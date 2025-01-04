using Progetta.Services;
using Progetta.Components;
using Microsoft.EntityFrameworkCore;
using Progetta.Entities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Progetta.Components.Account;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWX5edHRcRGdeUUNwX0A=");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDevExpressServerSideBlazorReportViewer();
builder.Services.AddDevExpressBlazor(options => {
    options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
    options.SizeMode = DevExpress.Blazor.SizeMode.Medium;
});
builder.Services.AddDevExpressServerSideBlazorReportViewer();
builder.Services.AddMvc();

builder.Services.AddSyncfusionBlazor();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddAuthentication(options => {
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddIdentityCookies(x =>
{
    x.ApplicationCookie.Configure(x => x.LoginPath = "/Login");
});

builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddDbContextFactory<ProjectContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("ProjectConnectionString"))
);

builder.Services
    .AddIdentityCore<User>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 0;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequiredUniqueChars = 0;
    })
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<ProjectContext>()
    .AddUserManager<UserManager<User>>()
    .AddSignInManager<SignInManager<User>>()
    .AddRoleManager<RoleManager<IdentityRole<int>>>()
    .AddDefaultTokenProviders();

builder.WebHost.UseStaticWebAssets();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

using (IServiceScope scope = app.Services.CreateScope())
{
    UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    RoleManager<IdentityRole<int>> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

    IdentityRole<int> administratorRole = await roleManager.FindByNameAsync("Administrator");
    if(administratorRole is null)
    {
        administratorRole = new IdentityRole<int>
        {
            Name = "Administrator"
        };

        await roleManager.CreateAsync(administratorRole);
    }

    IdentityRole<int> standardUserRole = await roleManager.FindByNameAsync("StandardUser");
    if (standardUserRole is null)
    {
        standardUserRole = new IdentityRole<int>
        {
            Name = "StandardUser"
        };

        await roleManager.CreateAsync(standardUserRole);
    }

    User administrator = await userManager.FindByNameAsync("Admin");
    if(administrator is null)
    {
        administrator = new User
        {
            Email = "admin@progetta.com",
            UserName = "Admin",
            FirstName = "Admin",
            LastName = "Admin"
        };

        administrator.PasswordHash = userManager.PasswordHasher.HashPassword(administrator, "admin");
        await userManager.CreateAsync(administrator);
        await userManager.AddToRoleAsync(administrator, "Administrator");
    }

    User standardUser = await userManager.FindByNameAsync("StandardUser");
    if(standardUser is null)
    {
        standardUser = new User
        {
            Email = "user@progetta.com",
            UserName = "User",
            FirstName = "Joe",
            LastName = "Doe"

        };

        standardUser.PasswordHash = userManager.PasswordHasher.HashPassword(standardUser, "user");
        await userManager.CreateAsync(standardUser);
        await userManager.AddToRoleAsync(standardUser, "StandardUser");
    }
}

app.Run();