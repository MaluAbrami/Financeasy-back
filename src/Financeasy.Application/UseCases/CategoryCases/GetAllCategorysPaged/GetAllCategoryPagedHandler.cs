using System.Linq.Expressions;
using Financeasy.Domain.DTO.Category;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.CategoryCases.GetAllCategorysPaged
{
    public class GetAllCategoryPagedHandler : IRequestHandler<GetAllCategorysPaged, GetAllCategorysPagedResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoryPagedHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<GetAllCategorysPagedResponse> Handle(GetAllCategorysPaged request, CancellationToken cancellationToken)
        {
            Expression<Func<Category, object>> expression = 
                request.OrderBy switch
                {
                    CategoryOrderBy.Name => x => x.Name,
                    _ => x => x.Name
                };

            List<CategoryResponseDTO> responseList = [];

            GetPagedBaseResponseDTO<Category> responsePaged = await _categoryRepository.GetPagedAsync(
                x => x.UserId == request.UserId,
                expression,
                request.Direction == SortDirection.Asc
                ? true
                : false,
                request.Pagination.Page,
                request.Pagination.PageSize
            );

            foreach(var category in responsePaged.List)
            {
                var categoryResponse = new CategoryResponseDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    Type = category.Type,
                    RecurrenceType = category.RecurrenceType
                };
                
                responseList.Add(categoryResponse);
            }

            return new GetAllCategorysPagedResponse 
            { 
                Categorys = responseList, 
                Pagination = new PaginationResponseBase 
                { 
                    Page = request.Pagination.Page, 
                    PageSize = request.Pagination.PageSize,
                    TotalItems = responsePaged.TotalItems,
                    TotalPages = (int)Math.Ceiling(
                        responsePaged.TotalItems / (double)request.Pagination.PageSize
                    )
                } 
            };
        }
    }
}