using CerteecStore.Application.Carts;
using CerteecStore.Application.Database;
using CerteecStore.Application.Products;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddTransient<ICartService, CartService>();

if (builder.Configuration["DatabaseType"] == "SQL")
{
    builder.Services.AddTransient<IProductRepository, ProductRepository>();
    builder.Services.AddTransient<ICartRepository, CartRepository>();
}
else if (builder.Configuration["DatabaseType"] == "Dapper")
{
    builder.Services.AddTransient<IProductRepository, DapperProductRepository>();
    builder.Services.AddTransient<ICartRepository, DapperCartRepository>();
}
else
{
    builder.Services.AddSingleton<InMemoryDatabase>(n =>
    {
        var database = new InMemoryDatabase();
        database.ReadProductsFromFile();
        return database;
    });
    builder.Services.AddTransient<IProductRepository, InMemoryProductRepository>();
    builder.Services.AddTransient<ICartRepository, InMemoryCartRepository>();
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
