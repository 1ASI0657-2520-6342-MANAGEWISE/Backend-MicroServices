using AidManager.Services.Payments.Application.Interfaces;
using AidManager.Services.Payments.Domain.Entities;
using AidManager.Services.Payments.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AidManager.Services.Payments.Infrastructure.Repositories
{
    public class PaymentDetailRepository : IPaymentDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentDetail> AddAsync(PaymentDetail paymentDetail)
        {
            await _context.PaymentDetails.AddAsync(paymentDetail);
            await _context.SaveChangesAsync(default); // Assuming default CancellationToken
            return paymentDetail;
        }

        public async Task<PaymentDetail> GetByIdAsync(int id)
        {
            return await _context.PaymentDetails.FindAsync(id);
        }

        // Implement other methods like UpdateAsync, DeleteAsync as needed
    }
}
