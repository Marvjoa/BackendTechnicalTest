using BackendTechnicalTest.API.Interfaces.Accounts;
using BackendTechnicalTest.API.Interfaces.Clients;
using BackendTechnicalTest.API.Interfaces.Transactions;
using BackendTechnicalTest.API.Services.Accounts;
using BackendTechnicalTest.API.Services.Clients;
using BackendTechnicalTest.API.Services.Transactions;
using BackendTechnicalTest.Infrastructure.Context;
using BackendTechnicalTest.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<BankDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// DI
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

// Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Swagger UI solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Importante: Mapear controladores
app.MapControllers();

app.Run();