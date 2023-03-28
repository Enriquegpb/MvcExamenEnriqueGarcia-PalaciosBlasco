using Microsoft.EntityFrameworkCore;
using MvcExamenEnriqueGarcia_PalaciosBlasco.Data;
using MvcExamenEnriqueGarcia_PalaciosBlasco.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string conectionString = builder.Configuration.GetConnectionString("SqlLibros");
builder.Services.AddDbContext<LibrosContext>(options =>
{
    options.UseSqlServer(conectionString);
});
builder.Services.AddTransient<RepositoryLibros>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Libros}/{action=Index}/{id?}");

app.Run();
