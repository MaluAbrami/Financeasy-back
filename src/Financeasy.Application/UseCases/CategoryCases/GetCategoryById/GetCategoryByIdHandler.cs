using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.CategoryCases.GetCategoryById
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, GetCategoryByIdResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoryByIdHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetCategoryByIdResponse> Handle(GetCategoryById request, CancellationToken cancellationToken)
        {
            var categoryExists = await _categoryRepository.GetByIdAsync(request.Id);

            if(categoryExists is null)
                throw new ArgumentException($"Categoria de id {request.Id} não foi encontrada.");

            if(categoryExists.UserId != request.UserId)
                throw new UnauthorizedAccessException("Usuário não pode realizar essa ação.");

            return new GetCategoryByIdResponse
            {
                Name = categoryExists.Name,
                Type = categoryExists.Type,
                IsFixed = categoryExists.IsFixed
            };
        }
    }
}