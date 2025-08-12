using Microsoft.EntityFrameworkCore;
using Evaluacion_Tecnica_Web.Datos;
using Evaluacion_Tecnica_Web.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ProductoContext>(options =>
    options.UseInMemoryDatabase("ProductList"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Producto}/{action=Index}/{id?}");

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ProductoContext>();

    // Add sample products if database is empty
    if (!context.Productos.Any())
    {
        context.Productos.AddRange(
            new Producto { nombre = "Laptop", Precio = 999.99m, categoria = "Electronica" },
            new Producto { nombre = "Mouse", Precio = 19.99m, categoria = "Electronica" },
            new Producto { nombre = "Silla", Precio = 149.99m, categoria = "Muebles" }
        );
        context.SaveChanges();
    }
}

app.Run();
