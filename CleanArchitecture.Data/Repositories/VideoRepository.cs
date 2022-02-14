using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class VideoRepository : RepositoryBase<Video>, IVideoRepository
    {
        public VideoRepository(StreamerDbContext context) : base(context)
        {
        }

        public async Task<Video> GetByNombre(string nombre)
        {
            return await context.Videos!.Where(x => x.Nombre == nombre).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Video>> GetByUsername(string userName)
        {
            return await context.Videos!.Where(x => x.CreatedBy == userName).ToListAsync();
        }
    }
}
