using AidManager.Services.Payments.Application.Commands;
using AidManager.Services.Payments.Application.Interfaces;
using AidManager.Services.Payments.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Services.Payments.Application.Handlers
{
    public class CreatePaymentDetailCommandHandler : IRequestHandler<CreatePaymentDetailCommand, int>
    {
        private readonly IPaymentDetailRepository _paymentDetailRepository;

        public CreatePaymentDetailCommandHandler(IPaymentDetailRepository paymentDetailRepository)
        {
            _paymentDetailRepository = paymentDetailRepository;
        }

        public async Task<int> Handle(CreatePaymentDetailCommand request, CancellationToken cancellationToken)
        {
            var paymentDetail = new PaymentDetail
            {
                UserId = request.UserId,
                CardHolderName = request.CardHolderName,
                CardNumber = request.CardNumber,
                ExpirationDate = request.ExpirationDate,
                CVV = request.CVV
            };

            var createdPaymentDetail = await _paymentDetailRepository.AddAsync(paymentDetail);
            return createdPaymentDetail.Id;
        }
    }
}
