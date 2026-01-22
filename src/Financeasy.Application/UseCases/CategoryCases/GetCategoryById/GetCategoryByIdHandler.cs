using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.CategoryCases.GetCategoryById
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, GetCategoryByIdResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryByIdHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<GetCategoryByIdResponse> Handle(GetCategoryById request, CancellationToken cancellationToken)
        {
            var categoryExists = await _categoryRepository.GetByIdAndUserId(request.Id, request.UserId, cancellationToken);

            if(categoryExists is null)
                throw new ArgumentException($"Categoria de id {request.Id} não foi encontrada.");

            return new GetCategoryByIdResponse
            {
                Name = categoryExists.Name,
                Type = categoryExists.Type,
                RecurrenceType = categoryExists.RecurrenceType
            };
        }
    }
}