using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IVideoRepository : IAsyncRepository<Video>
    {
        Task<Video> GetByNombre(string nombre);
        Task<IEnumerable<Video>> GetByUsername(string userName);
    }
}
