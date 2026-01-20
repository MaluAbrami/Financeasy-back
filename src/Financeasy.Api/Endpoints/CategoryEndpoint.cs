using Financeasy.Application.UseCases.CategoryCases.CreateCategory;
using Financeasy.Application.UseCases.CategoryCases.DeleteCategory;
using Financeasy.Application.UseCases.CategoryCases.GetAllCategorys;
using Financeasy.Application.UseCases.CategoryCases.GetAllCategorysPaged;
using Financeasy.Application.UseCases.CategoryCases.GetAllFixedCategorys;
using Financeasy.Application.UseCases.CategoryCases.GetCategoryById;
using Financeasy.Domain.DTO.Category;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Api.Endpoints
{
    public static class CategoryEndpoint
    {
        public static RouteGroupBuilder MapCategorys(this RouteGroupBuilder group)
        {
            group.MapPost("", CreateCategory)
                .RequireAuthorization();
                
            group.MapGet("/all/{page}/{pageSize}/{orderBy}/{direction}", GetAllCategorysPaged)
                .RequireAuthorization();

            group.MapGet("/all", GetAllCategorys)
                .RequireAuthorization();

            group.MapGet("/all-fixed", GetAllFixedCategorys)
                .RequireAuthorization();

            group.MapGet("/{id}", GetCategoryById)
                .RequireAuthorization();

            group.MapDelete("/{id}", DeleteCategory)
                .RequireAuthorization();

            return group;
        }

        private static async Task<IResult> CreateCategory(CreateCategoryRequestDTO request, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new CreateCategoryCommand { UserId = Guid.Parse(userId), Name = request.Name, Type = request.Type, RecurrenceType = request.RecurrenceType} );

            return Results.Created();
        }

        private static async Task<IResult> GetAllCategorysPaged(
            HttpContext context, 
            IMediator mediator,
            int page = 1, 
            int pageSize = 10, 
            CategoryOrderBy orderBy = CategoryOrderBy.Name, 
            SortDirection direction = SortDirection.Asc)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            var response = await mediator.Send(new GetAllCategorysPaged { UserId = Guid.Parse(userId), Pagination = new PaginationRequestBase { Page = page, PageSize = pageSize }, OrderBy = orderBy, Direction = direction } );

            return Results.Ok(response);
        }

        private static async Task<IResult> GetAllCategorys(HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            var response = await mediator.Send(new GetAllCategorys { UserId = Guid.Parse(userId) } );

            return Results.Ok(response);
        }

        private static async Task<IResult> GetAllFixedCategorys(HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            var response = await mediator.Send(new GetAllFixedCategorys { UserId = Guid.Parse(userId) });

            return Results.Ok(response);
        }

        private static async Task<IResult> GetCategoryById(Guid id, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            var response = await mediator.Send(new GetCategoryById { Id = id, UserId = Guid.Parse(userId) } );

            return Results.Ok(response);
        }

        private static async Task<IResult> DeleteCategory(Guid id, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            var response = await mediator.Send(new DeleteCategoryCommand { Id = id, UserId = Guid.Parse(userId) } );

            return Results.NoContent();
        }
    }
}