using Financeasy.Domain.DTO;
using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.CategoryCases.GetAllCategorys
{
    public class GetAllCategoryHandler : IRequestHandler<GetAllCategorys, GetAllCategorysResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCategoryHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllCategorysResponse> Handle(GetAllCategorys request, CancellationToken cancellationToken)
        {
            List<CategoryResponseDTO> responseList = [];

            var categorysList = await _categoryRepository.FindAsync(x => x.UserId == request.UserId);

            foreach(var category in categorysList)
            {
                var categoryResponse = new CategoryResponseDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    Type = category.Type,
                    IsFixed = category.IsFixed,
                    Recurrence = category.Recurrence
                };
                
                responseList.Add(categoryResponse);
            }

            return new GetAllCategorysResponse { Categorys = responseList } ;
        }
    }
}