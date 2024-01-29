using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ToTable.Contract;
using ToTable.Controllers;
using ToTable.Interfaces;
using ToTable.Middleware;
using ToTable.Models;
using ToTable.Services;
using ToTable.Validator;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
var allowedOrigin = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ToTableDbContext>(opt =>
    opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IWaiterService, WaiterService>();
builder.Services.AddScoped<IValidator<OrderDto>, OrderValidator>();
builder.Services.AddScoped<IValidator<OrderItemDto>, OrderItemValidator>();
builder.Services.AddScoped<IValidator<ProductDto>, ProductValidator>();
builder.Services.AddScoped<IValidator<RestaurantDto>, RestaurantValidator>();
builder.Services.AddScoped<IValidator<TableDto>, TableValidator>();
builder.Services.AddScoped<IValidator<WaiterDto>, WaiterValidator>();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(120);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("myAppCors", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Uncomment to use the global exception handling middleware
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseCors("myAppCors");
app.UseHttpsRedirection();
app.UseSession();


app.UseAuthorization();
app.MapControllers();

app.Run();