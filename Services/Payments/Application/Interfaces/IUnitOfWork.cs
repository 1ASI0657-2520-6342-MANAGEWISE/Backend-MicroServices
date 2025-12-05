using System.Threading.Tasks;

namespace AidManager.Services.Payments.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPaymentDetailRepository PaymentDetails { get; }
        
        Task<int> CompleteAsync();
    }
}