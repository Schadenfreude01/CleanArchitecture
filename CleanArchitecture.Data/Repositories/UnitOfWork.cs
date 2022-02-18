using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence;
using System.Collections;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable repositories;
        private readonly StreamerDbContext context;

        private IVideoRepository videoRepository;
        private IStreamerRepository streamerRepository;

        public IVideoRepository VideoRepository => videoRepository ??= new VideoRepository(context);
        public IStreamerRepository StreamerRepository => streamerRepository ??= new StreamerRepository(context);

        public UnitOfWork(StreamerDbContext context)
        {
            this.context = context;
        }

        public StreamerDbContext StreamerDbContext => context;

        public async Task<int> Complete()
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Err", ex);
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if(repositories == null) repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if(!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var respositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), context);

                repositories.Add(type, respositoryInstance);
            }

            return (IAsyncRepository<TEntity>)repositories[type];
        }
    }
}
