using Spill.Core.Web.Components;
using Spill.Core.Web.DBModels;
using Spill.Core.Web.Models;
using Spill.Core.Web.DBModels.Interfaces;
using Spill.Core.Web.Utils;
using Spill.Core.Web.Services.MetaServices;
using System.Configuration;
using Spill.Core.Web.Application;
using Spill.Core.Web.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDatabaseService, DatabaseService>(provider =>
        new DatabaseService(ConnectionStringUtils.ConnectionString)); 

builder.Services.AddScoped<ILeadService, LeadService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<ILeadsApplication, LeadsApplication>();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
