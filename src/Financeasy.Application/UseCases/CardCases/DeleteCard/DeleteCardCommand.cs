using MediatR;

namespace Financeasy.Application.UseCases.CardCases.DeleteCard
{
    public record DeleteCardCommand : IRequest<DeleteCardCommand>
    {
        public Guid UserId { get; set; }
        public Guid CardId { get; set; }
    }
}