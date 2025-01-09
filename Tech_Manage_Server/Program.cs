using Domain.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Tech_Manage_Server.Application.Helpers;
using Tech_Manage_Server.Data;
using Tech_Manage_Server.Domain.Interface;
using Tech_Manage_Server.Helpers;
using Tech_Manage_Server.Infrastructure.Implementation;
using Tech_Manage_Server.Models;
using Tech_Manage_Server.Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ManageDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Đăng ký Identity
builder.Services.AddIdentity<ApplicationUser, Role>(opt =>
{
    opt.User.RequireUniqueEmail = false;
    opt.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<ManageDBContext>()
    .AddDefaultTokenProviders();

// Cấu hình Authentication với JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(opt =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:4100") // URL của Angular
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); // Cho phép gửi credentials (cookies, auth headers)
    });
});

// Add services to the container.
builder.Services.AddSignalR();

// Automapper
builder.Services.AddAutoMapper(typeof(MappingProfiles));
//builder.Services.AddAutoMapper(typeof(Program));

// Life cycle DI: AddScoped()
builder.Services.AddScoped<IRepairRepository, RepairRepository>();
builder.Services.AddScoped<IRepairItemRepository, RepairItemRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

var app = builder.Build();

app.UseCors("AllowSpecificOrigins"); // Áp dụng CORS

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources/Images")),
    RequestPath = "/Resources/Images"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Cấp phép cho frontend
app.UseCors(policy =>
    policy.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
);

// sử dụng xác thực và phân quyền
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ManageDBContext>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await DbInitializer.Inittialize(userManager);
}
catch (Exception ex)
{
    logger.LogError(ex, "A problem with migration for database");
}

app.MapHub<ChatHub>("/chatHub");
app.Run();
