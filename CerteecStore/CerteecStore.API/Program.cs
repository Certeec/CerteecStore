using CerteecStore.Application.Carts;
using CerteecStore.Application.Database;
using CerteecStore.Application.Products;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<InMemoryDatabase>(n =>
{
    var database = new InMemoryDatabase();
    database.ReadProductsFromFile();
    return database;
});
builder.Services.AddTransient<IProductRepository, InMemoryProductRepository>();
builder.Services.AddTransient<ICartRepository, InMemoryCartRepository>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<IProductService, ProductService>();



var app = builder.Build();
//app.Services.GetService<InMemoryDatabase>().ReadProductsFromFile();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
