using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TMA3A.Data;
using TMA3A.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using TMA3A.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<TMA3AContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TMA3AContext") ?? throw new InvalidOperationException("Connection string 'TMA3AContext' not found.")));

builder.Services.AddDefaultIdentity<User>(options => {
    options.SignIn.RequireConfirmedAccount = true; 
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<TMA3AContext>();

builder.Services.AddRazorPages(options => {
    options.Conventions.AllowAnonymousToFolder("/PartPicker");
    options.Conventions.AllowAnonymousToFolder("/Orders");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TMA3AContext>();
    context.Database.EnsureCreated();
    ReadImages.Initialize(app.Services.GetService(typeof(IWebHostEnvironment)), context);
    await InitData.Initialize(services);
    TestData.Initialize(services);
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
