using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces;
using OrdersAPI.Core.Services.OrderItemsServices;
using OrdersAPI.Core.Services.OrderServices;
using OrdersAPI.Infrastructure.DatabaseContext;
using OrdersAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole().AddDebug();

builder.Services.AddControllers(options =>
{
	options.Filters.Add(new ProducesAttribute("application/json"));
	options.Filters.Add(new ConsumesAttribute("application/json"));
});

var apiVersioningBuilder = builder.Services.AddApiVersioning(config =>
{
	config.ApiVersionReader = new UrlSegmentApiVersionReader();
	config.DefaultApiVersion = new ApiVersion(1, 0);
	config.AssumeDefaultVersionWhenUnspecified = true;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));
	options.SwaggerDoc("v1", new OpenApiInfo { Title = "OrdersAPI", Version = "1.0" });
});

builder.Services.AddVersionedApiExplorer(options =>
{
	options.GroupNameFormat = "'v'VVV";
	options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IOrderGetterService, OrderGetterService>();
builder.Services.AddScoped<IOrderAdderService, OrderAdderService>();
builder.Services.AddScoped<IOrderUpdaterService, OrderUpdaterService>();
builder.Services.AddScoped<IOrderDeleterService, OrderDeleterService>();

builder.Services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();
builder.Services.AddScoped<IOrderItemGetterService, OrderItemGetterService>();
builder.Services.AddScoped<IOrderItemAdderService, OrderItemAdderService>();
builder.Services.AddScoped<IOrderItemUpdaterService, OrderItemUpdaterService>();
builder.Services.AddScoped<IOrderItemDeleterService, OrderItemDeleterService>();

var app = builder.Build();

app.UseHsts();
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
	options.SwaggerEndpoint("/swagger/v1/swagger.json", "OrdersAPI v1.0");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
