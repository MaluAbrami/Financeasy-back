using Financeasy.Application.UseCases.CategoryCases.GetAllCategorys;
using Financeasy.Domain.DTO.Category;
using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.CategoryCases.GetAllFixedCategorys
{
    public class GetAllFixedCategorysHandler : IRequestHandler<GetAllFixedCategorys, GetAllCategorysRespone>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllFixedCategorysHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<GetAllCategorysRespone> Handle(GetAllFixedCategorys request, CancellationToken cancellationToken)
        {
            var categorys = await _categoryRepository.FindAsync(x => x.UserId == request.UserId && x.RecurrenceType != RecurrenceType.None, cancellationToken);

            List<CategoryResponseDTO> listResponse = [];

            foreach (var category in categorys)
            {
                CategoryResponseDTO response = new CategoryResponseDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    Type = category.Type,
                    RecurrenceType = category.RecurrenceType
                };

                listResponse.Add(response);
            }

            return new GetAllCategorysRespone { Categorys = listResponse };
        }
    }
}