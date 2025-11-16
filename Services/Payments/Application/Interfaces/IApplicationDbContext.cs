using AidManager.Services.Payments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AidManager.Services.Payments.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<PaymentDetail> PaymentDetails { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
