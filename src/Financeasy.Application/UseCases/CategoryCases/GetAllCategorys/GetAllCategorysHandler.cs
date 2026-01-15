using Financeasy.Domain.DTO.Category;
using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.CategoryCases.GetAllCategorys
{
    public class GetAllCategorysHandler : IRequestHandler<GetAllCategorys, GetAllCategorysRespone>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategorysHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<GetAllCategorysRespone> Handle(GetAllCategorys request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.FindAsync(x => x.UserId == request.UserId);

            List<CategoryResponseDTO> listResponse = [];

            foreach (var category in categories)
            {
                var categoryResponse = new CategoryResponseDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    Type = category.Type,
                    RecurrenceType = category.RecurrenceType
                };

                listResponse.Add(categoryResponse);
            }

            return new GetAllCategorysRespone { Categorys = listResponse };
        }
    }
}