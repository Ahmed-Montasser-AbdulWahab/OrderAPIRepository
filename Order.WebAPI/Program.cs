using Microsoft.EntityFrameworkCore;
using Orders.Entities.DatabaseContext;
using Orders.Entities.ModelEntities;
using Orders.ServiceContracts;
using Orders.Services;
using Repositories;
using RepositoryContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("ApiDB"))
    );

builder.Services.AddScoped<IAddRepository<Order>, OrdersAddRepository>();
builder.Services.AddScoped<IGetRepository<Order>,OrdersGetRepository>();
builder.Services.AddScoped<IEditRepository<Order>, OrdersEditRepository>();
builder.Services.AddScoped<IDeleteRepository<Order>, OrdersDeleteRepository>();
builder.Services.AddScoped<IAddRepository<OrderItem>, OrderItemsAddRepository>();
builder.Services.AddScoped<IGetRepository<OrderItem>, OrderItemsGetRepository>();
builder.Services.AddScoped<IEditRepository<OrderItem>, OrderItemsEditRepository>();
builder.Services.AddScoped<IDeleteRepository<OrderItem>, OrderItemsDeleteRepository>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IOrderItemsService, OrderItemsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHsts();

app.UseHttpsRedirection();

app.UseRouting();


app.MapControllers();

app.Run();
