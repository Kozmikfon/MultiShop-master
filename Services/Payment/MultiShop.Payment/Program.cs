using MultiShop.Payment.Services.Abstract;
using MultiShop.Payment.Services.Concrete;
using MultiShop.Payment.Settings;

var builder = WebApplication.CreateBuilder(args);

// --- 1. ADIM: CORS POLÝTÝKASINI TANIMLA (builder.Build'den önce) ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Add services to the container.
builder.Services.Configure<IyzicoSettings>(builder.Configuration.GetSection("IyzicoSettings"));

builder.Services.AddScoped<IPaymentService, IyzicoPaymentService>();

builder.Services.AddHttpClient<IOrderService, OrderService>(opt =>
{
    // KRÝTÝK: 'https' yerine 'http' yazýyoruz. Port 7072 olarak kalýyor.
    // Çünkü senin Ordering servisin 7072'de HTTP dinliyor.
    opt.BaseAddress = new Uri("http://localhost:7072/");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        // Geliþtirme ortamýnda sertifika kontrollerini bypass etmeye devam edelim
        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// --- 2. ADIM: CORS MIDDLEWARE'I AKTÝF ET (Sýralama Önemli!) ---
app.UseHttpsRedirection();

app.UseRouting(); // Yönlendirmeyi aç

app.UseCors("AllowAll"); // "AllowAll" politikasýyla kapýlarý aç

app.UseAuthorization();

app.MapControllers();

app.Run();