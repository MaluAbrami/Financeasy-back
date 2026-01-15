using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.CategoryCases.DeleteCategory
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<DeleteCategoryCommand> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryExists = await _categoryRepository.GetByIdAndUserId(request.Id, request.UserId);

            if(categoryExists is null)
                throw new ArgumentException($"Categoria de id {request.Id} não foi encontrada.");

            _categoryRepository.Delete(categoryExists);
            await _unitOfWork.SaveChangesAsync();

            return request;
        }
    }
}