using MediatR;

namespace Financeasy.Application.UseCases.CardCases.DeleteCard
{
    public record DeleteCardCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid CardId { get; set; }
    }
}