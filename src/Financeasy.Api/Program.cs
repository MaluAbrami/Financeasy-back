using System.Text.Json.Serialization;
using Financeasy.Api.Endpoints;
using Financeasy.CrossCutting.DependencyInjections;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddCorsInjections();

builder.Services.AddBehaviors();

builder.Services.AddTokenJwt(builder.Configuration);

builder.Services.AddRepositories();

builder.Services.AddServices();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.UseCors("AllowAll");

app.Use(async (ctx, next) =>
{
    try
    {
        await next();
    }
    catch (FluentValidation.ValidationException ex)
    {
        var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
        ctx.Response.StatusCode = 400;
        await ctx.Response.WriteAsJsonAsync(errors);
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("/users")
    .WithTags("Users")
    .MapUserEndpoints();

app.MapGroup("/categorys")
    .WithTags("Categorys")
    .MapCategorys();

app.MapGroup("/bank-accounts")
    .WithTags("Bank Accounts")
    .MapBankAccounts();

app.Run();