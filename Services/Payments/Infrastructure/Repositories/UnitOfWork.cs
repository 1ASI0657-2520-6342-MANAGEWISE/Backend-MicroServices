using AidManager.Services.Payments.Application.Interfaces;
using AidManager.Services.Payments.Infrastructure.Persistence;
using System.Threading.Tasks;

namespace AidManager.Services.Payments.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IPaymentDetailRepository PaymentDetails { get; }

        public UnitOfWork(ApplicationDbContext context, IPaymentDetailRepository paymentDetailRepository)
        {
            _context = context;
            PaymentDetails = paymentDetailRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}