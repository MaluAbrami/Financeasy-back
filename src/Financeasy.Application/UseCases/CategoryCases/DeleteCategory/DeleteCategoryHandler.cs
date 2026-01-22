using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.CategoryCases.DeleteCategory
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryExists = await _categoryRepository.GetByIdAndUserId(request.Id, request.UserId, cancellationToken);

            if(categoryExists is null)
                throw new ArgumentException($"Categoria de id {request.Id} não foi encontrada.");

            _categoryRepository.Delete(categoryExists);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}