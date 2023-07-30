using DecisionTree.MVC.Infrastructure.DAL;
using DecisionTree.MVC.Infrastructure.Repositories.Interfaces;

namespace DecisionTree.MVC.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IHierarchyItemRepository HierarchyItemRepository { get; set; }

        private readonly AppDbContext _ctx;

        public UnitOfWork(
            AppDbContext ctx,
            IHierarchyItemRepository hiRepository
            )
        {
            _ctx = ctx;
            HierarchyItemRepository = hiRepository;
        }

        public async Task CommitChangesAsync(CancellationToken cancellationToken = default) => await _ctx.SaveChangesAsync(cancellationToken);

        private bool _disposed;

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                // Dispose managed resources.
                await _ctx.DisposeAsync();

                // Dispose any unmanaged resources here...
                _disposed = true;
            }
        }
    }

    public interface IUnitOfWork : IAsyncDisposable
    {
        //IRepository<Catalog, int> CatalogRepository { get; set; }
        //IRepository<Order, int> OrderRepository { get; set; }
        //IRepository<OrderItem, int> OrderItemRepository { get; set; }
        //IRepository<HierarchyItem, int> HierarchyItemRepository { get; set; }
        IHierarchyItemRepository HierarchyItemRepository { get; set; }
        Task CommitChangesAsync(CancellationToken cancellationToken = default);

    }
}
