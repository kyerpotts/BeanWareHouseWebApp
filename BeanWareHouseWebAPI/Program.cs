using BeanWareHouseWebAPI.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<BeanWarehouseSingleton>(BeanWarehouseSingleton.Instance);
builder.Services.AddSingleton<ITempDataDictionaryFactory, TempDataDictionaryFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
