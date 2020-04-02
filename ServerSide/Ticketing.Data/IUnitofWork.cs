using System.Threading.Tasks;

namespace Ticketing.Data
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();

    }

   public class UnitOfWork : IUnitOfWork
    {
        private readonly TicketingDbContext _context;

        public UnitOfWork(TicketingDbContext context)
        {
            _context = context;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}