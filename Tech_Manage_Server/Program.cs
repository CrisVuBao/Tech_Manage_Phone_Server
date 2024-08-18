using Microsoft.EntityFrameworkCore;
using Tech_Manage_Server.Data;
using Tech_Manage_Server.Repositories.Implementation;
using Tech_Manage_Server.Repositories.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ManageDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Automapper
builder.Services.AddAutoMapper(typeof(Program));

// Life cycle DI: AddScoped()
builder.Services.AddScoped<IRepairRepository, RepairRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Cấp phép cho frontend
app.UseCors(policy =>
    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
);

app.UseAuthorization();

app.MapControllers();

app.Run();
