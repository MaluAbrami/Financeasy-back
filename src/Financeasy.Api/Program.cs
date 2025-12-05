using Financeasy.Api.Endpoints;
using Financeasy.Application.UseCases.RegisterUser;
using Financeasy.Domain.interfaces;
using Financeasy.Infra.Persistence;
using Financeasy.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<FinanceasyDbContext>(options =>
    options.UseMySQL(connectionString!));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly);
});

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("/users")
    .WithTags("Users")
    .MapUserEndpoints();

app.Run();