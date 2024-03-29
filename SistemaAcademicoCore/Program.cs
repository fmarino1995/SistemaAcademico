using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoApplication;
using SistemaAcademicoCore.Areas.Identity;
using SistemaAcademicoCore.Shared;
using SistemaAcademicoData.Context;
using SistemaAcademicoInfrastructure;
using SistemaAcademicoInfrastructure.Services;
using System.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var appsettings = builder.Configuration.GetSection("AppSettings");

#if DEBUG
var useConnection = appsettings.GetValue<string>("ConexaoDebug");
#elif DEVELOPMENT
var useConnection = appsettings.GetValue<string>("ConexaoDevelopment");
#else
var useConnection = appsettings.GetValue<string>("ConexaoDebug");
#endif

var connectionString = builder.Configuration.GetConnectionString(useConnection);

builder.Services.AddDbContext<SistemaAcademicoContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<SistemaAcademicoContext>()
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy(Policies.IsAdmin, Policies.IsAdminPolicy());
    config.AddPolicy(Policies.IsAluno, Policies.IsAlunoPolicy());
    config.AddPolicy(Policies.IsProfessor, Policies.IsProfessorPolicy());
});

builder.Services.Configure<IdentityOptions>(options => 
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var emailSettings = builder.Configuration.GetSection("EMailSettings");

builder.Services.Configure<EMailSettings>(emailSettings);

builder.Services.AddLocalization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


app.UseRequestLocalization("pt-BR");

app.Run();
