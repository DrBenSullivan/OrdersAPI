using Microsoft.EntityFrameworkCore;
using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces;
using OrdersAPI.Core.Services.OrderItemsServices;
using OrdersAPI.Core.Services.OrderServices;
using OrdersAPI.Infrastructure.DatabaseContext;
using OrdersAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole().AddDebug(); 

builder.Services.AddControllers()
	.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddEndpointsApiExplorer();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
