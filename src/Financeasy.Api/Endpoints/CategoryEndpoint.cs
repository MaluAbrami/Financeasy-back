using Financeasy.Application.UseCases.CategoryCases.CreateCategory;
using Financeasy.Domain.DTO;
using MediatR;

namespace Financeasy.Api.Endpoints
{
    public static class CategoryEndpoint
    {
        public static RouteGroupBuilder MapCategorys(this RouteGroupBuilder group)
        {
            group.MapPost("", CreateCategory)
                .RequireAuthorization();

            return group;
        }

        private static async Task<IResult> CreateCategory(CreateCategoryRequestDTO request, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new CreateCategoryCommand { UserId = Guid.Parse(userId), Name = request.Name, Type = request.Type, IsFixed = request.IsFixed, Recurrence = request.Recurrence } );

            return Results.Created();
        }
    }
}