using Demo.Infrastructure;
using Demo.Tables.Employees;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connStr)
);

builder.Services.AddScoped<IEmployeeTableRepository, EmployeeTableRepository>();
builder.Services.AddScoped<IEmployeeTableService, EmployeeTableService>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run("https://localhost:5001");
