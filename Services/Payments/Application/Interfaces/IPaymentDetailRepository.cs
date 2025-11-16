using AidManager.Services.Payments.Domain.Entities;
using System.Threading.Tasks;

namespace AidManager.Services.Payments.Application.Interfaces
{
    public interface IPaymentDetailRepository
    {
        Task<PaymentDetail> GetByIdAsync(int id);
        Task<PaymentDetail> AddAsync(PaymentDetail paymentDetail);
        // Add other methods like UpdateAsync, DeleteAsync as needed
    }
}
