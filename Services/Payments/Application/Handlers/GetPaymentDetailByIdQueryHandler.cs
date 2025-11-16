using AidManager.Services.Payments.Application.DTOs;
using AidManager.Services.Payments.Application.Interfaces;
using AidManager.Services.Payments.Application.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Services.Payments.Application.Handlers
{
    public class GetPaymentDetailByIdQueryHandler : IRequestHandler<GetPaymentDetailByIdQuery, PaymentDetailDto>
    {
        private readonly IPaymentDetailRepository _paymentDetailRepository;

        public GetPaymentDetailByIdQueryHandler(IPaymentDetailRepository paymentDetailRepository)
        {
            _paymentDetailRepository = paymentDetailRepository;
        }

        public async Task<PaymentDetailDto> Handle(GetPaymentDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var paymentDetail = await _paymentDetailRepository.GetByIdAsync(request.Id);

            if (paymentDetail == null)
            {
                // Handle not found scenario, perhaps return null or throw an exception
                return null;
            }

            return new PaymentDetailDto(
                paymentDetail.Id,
                paymentDetail.UserId,
                paymentDetail.CardHolderName,
                paymentDetail.CardNumber,
                paymentDetail.ExpirationDate,
                paymentDetail.CVV
            );
        }
    }
}
