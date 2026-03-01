using Microsoft.EntityFrameworkCore;
using MultiShop.Seller.Businesslayer.Abstract;
using MultiShop.Seller.Businesslayer.Concrete;
using MultiShop.Seller.Businesslayer.Mapping;
using MultiShop.Seller.DataAccessLayer.Abstract;
using MultiShop.Seller.DataAccessLayer.Context;
using MultiShop.Seller.DataAccessLayer.EntityFramework;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SellerContext>();

builder.Services.AddScoped<IVendorDal, EfVendordal>();
builder.Services.AddScoped<IVendorService, VendorManager>();

builder.Services.AddAutoMapper(typeof(GeneralMapping));

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
