using ChalkboardChat.BLL.Services;
using ChalkboardChat.DAL;
using ChalkboardChat.DAL.Data;
using ChalkboardChat.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection")));

builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MessageConnection")));

builder.Services.AddScoped<IMessageRepository, MessageRepository>();

builder.Services.AddScoped<IMessageService, MessageService>();


builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // ===== Password settings =====
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;

    // ===== User settings =====
    options.User.RequireUniqueEmail = false;
})
.AddEntityFrameworkStores<AuthDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();

//DATA lager
// DbContext AppDbContext identityDbConyext
//Migration
//Model movie, oerson, produkt
//Repository
//SQL kod

//BLL Applikations lager
// All affärslogik
//Service klasser
//Interface - IService
//Dto
//Valideringsregler
//API anrop

//UI
//Razor pages
//Om MVC controller (ingen affärslogik)
//ViewModel (om man behöver flera objekt i en vy)
// anroper services i BLL
// ModelState