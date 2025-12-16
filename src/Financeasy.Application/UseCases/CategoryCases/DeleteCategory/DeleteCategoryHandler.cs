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
            var categoryExists = await _categoryRepository.GetByIdAsync(request.Id);

            if(categoryExists is null)
                throw new ArgumentException($"Categoria de id {request.Id} não foi encontrada.");

            if(categoryExists.UserId != request.UserId)
                throw new UnauthorizedAccessException("Usuário não pode realizar essa ação.");

            _categoryRepository.Delete(categoryExists);
            await _unitOfWork.SaveChangesAsync();

            return request;
        }
    }
}