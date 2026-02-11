var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

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