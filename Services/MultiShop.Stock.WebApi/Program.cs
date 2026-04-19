using MassTransit;
using Microsoft.Extensions.Options;
using MultiShop.Stock.BusinessLayer.Abstract;
using MultiShop.Stock.BusinessLayer.Concrete;
using MultiShop.Stock.BusinessLayer.Consumers;
using MultiShop.Stock.BusinessLayer.Mapping;
using MultiShop.Stock.DataAccessLayer.Abstract;
using MultiShop.Stock.DataAccessLayer.EntityFramework;
using MultiShop.Stock.EntityLayer.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// AutoMapper Kaydý
builder.Services.AddAutoMapper(typeof(GeneralMapping));

// Servis Kayýtlarý
builder.Services.AddScoped<IStockDal, MongoStockDal>();
builder.Services.AddScoped<IStockService, StockManager>();


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProductCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        // builder.Configuration["RabbitMQUrl"] yerine doðrudan elle yazýyoruz
        cfg.Host("localhost", "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint("stock-product-created-queue", e =>
        {
            e.ConfigureConsumer<ProductCreatedConsumer>(context);
        });
    });
});


builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

// 2. IDatabaseSettings istendiðinde, yukarýda eþleþen deðerleri döndür
builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
