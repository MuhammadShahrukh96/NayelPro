using NayelPro.Models;

namespace NayelPro.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context, IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _context = context;
            Products = productRepo;
            Categories = categoryRepo;
        }

        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }

}
