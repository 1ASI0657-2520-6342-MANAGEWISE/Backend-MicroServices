using AidManager.Services.Payments.Application.DTOs;
using MediatR;

namespace AidManager.Services.Payments.Application.Queries
{
    public record GetPaymentDetailByIdQuery(int Id) : IRequest<PaymentDetailDto>;
}
