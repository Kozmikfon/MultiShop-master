using MultiShop.Payment.Services.Abstract;
using MultiShop.Payment.Services.Concrete;
using MultiShop.Payment.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 1. Ayarlarý appsettings.json'dan IyzicoSettings sýnýfýna baðla
builder.Services.Configure<IyzicoSettings>(builder.Configuration.GetSection("IyzicoSettings"));

// 2. Servisi Interface üzerinden register et
builder.Services.AddScoped<IPaymentService, IyzicoPaymentService>();

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
