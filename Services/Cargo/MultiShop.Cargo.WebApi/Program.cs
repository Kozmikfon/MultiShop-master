using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.BusinessLayer.Concrete;
using MultiShop.Cargo.BusinessLayer.Mapping;
using MultiShop.Cargo.BusinessLayer.Settings;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.EntityFramework;
using MultiShop.Cargo.WebApi.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.Audience = "ResourceCargo";
    opt.RequireHttpsMetadata = false;
});

builder.Services.AddDbContext<CargoContext>();
builder.Services.AddScoped<ICargoCompanyDal, EfCargoCompanyDal>();
builder.Services.AddScoped<ICargoCompanyService, CargoCompanyManager>();
builder.Services.AddScoped<ICargoCustomerDal, EfCargoCustomerDal>();
builder.Services.AddScoped<ICargoCustomerService, CargoCustomerManager>();
builder.Services.AddScoped<ICargoDetailDal, EfCargoDetailDal>();
builder.Services.AddScoped<ICargoDetailService, CargoDetailManager>();
builder.Services.AddScoped<ICargoOperationDal, EfCargoOperationDal>();
builder.Services.AddScoped<ICargoOperationService, CargoOperationManager>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IShipinkService, ShipinkManager>();

builder.Services.AddAutoMapper(typeof(GeneralMapping));

builder.Services.Configure<ShipinkSettings>(builder.Configuration.GetSection("ShipinkSettings"));
builder.Services.AddScoped<IShipinkSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<ShipinkSettings>>().Value;
});

// 3. MASSTRANSIT & RABBITMQ AYARLARI
builder.Services.AddMassTransit(x =>
{
    // Consumer'ý MassTransit'e tanýtýyoruz
    x.AddConsumer<OrderPaidConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        // Docker kullanýyorsan "rabbitmq" veya yereldeysen "localhost"
        cfg.Host(builder.Configuration["RabbitMQUrl"] ?? "rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        // Kuyruk adýný ve dinleyecek Consumer'ý eþleþtiriyoruz
        cfg.ReceiveEndpoint("order-paid-queue", e =>
        {
            e.ConfigureConsumer<OrderPaidConsumer>(context);
        });
    });
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
