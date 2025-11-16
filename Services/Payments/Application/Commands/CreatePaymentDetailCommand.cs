using MediatR;

namespace AidManager.Services.Payments.Application.Commands
{
    public record CreatePaymentDetailCommand(
        int UserId,
        string CardHolderName,
        string CardNumber,
        string ExpirationDate,
        string CVV
    ) : IRequest<int>;
}
