using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.CategoryCases.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var duplicateName = await _categoryRepository.FindAsync(x => x.Name == request.Name && x.UserId == request.UserId, cancellationToken);
            if(duplicateName.Any())
                throw new ArgumentException("Já existe uma categoria com esse nome.");

            var newCategory = new Category(request.UserId, request.Name, request.Type);

            await _categoryRepository.AddAsync(newCategory, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newCategory.Id;
        }
    }
}