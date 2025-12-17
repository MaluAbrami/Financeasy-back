using System.Linq.Expressions;
using Financeasy.Domain.DTO;
using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.GetAllFinancialByUser
{
    public class GetAllFinancialByUserHandler : IRequestHandler<GetAllFinancialByUser, GetAllFinancialResponse>
    {
        private readonly IFinancialEntryRepository _financialRepository;

        public GetAllFinancialByUserHandler(IFinancialEntryRepository financialRepository)
        {
            _financialRepository = financialRepository;
        }

        public async Task<GetAllFinancialResponse> Handle(GetAllFinancialByUser request, CancellationToken cancellationToken)
        {
            Expression<Func<FinancialEntry, object>> orderExpression =
                request.OrderBy switch
                {
                    FinancialEntryOrderBy.Amount => x => x.Amount,
                    FinancialEntryOrderBy.Category => x => x.CategoryName,
                    FinancialEntryOrderBy.Date => x => x.Date,
                    FinancialEntryOrderBy.Type => x => x.Type,
                    _ => x => x.Date
                };

            GetPagedBaseResponseDTO<FinancialEntry> responsePaged = await _financialRepository.GetPagedAsync(
                x => x.UserId == request.UserId,
                orderExpression,
                request.Direction == SortDirection.Asc
                ? true
                : false,
                request.Pagination.Page,
                request.Pagination.PageSize
            );

            List<FinancialResponseDTO> listResponse = [];
            if (responsePaged.List.Any())
            {
                foreach (var financial in responsePaged.List)
                {
                    FinancialResponseDTO responseDTO = new FinancialResponseDTO
                    {
                        Id = financial.Id,
                        Amount = financial.Amount,
                        Description = financial.Description,
                        Date = financial.Date,
                        CategoryName = financial.CategoryName,
                        Type = financial.Type,
                        IsFixed = financial.IsFixed
                    };

                    listResponse.Add(responseDTO);
                }
            }

            return new GetAllFinancialResponse 
            { 
                FinancialsByUser = listResponse, 
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