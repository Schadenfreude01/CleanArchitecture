using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger)
        {
            if(!context.Streamers!.Any())
            {
                context.Streamers!.AddRange(GetPreconfiguredStreamer());
                await context.SaveChangesAsync();
                logger.LogInformation("Estamos insertando un seed a Streamer {context}", typeof(StreamerDbContext).Name);
            }
        }

        private static IEnumerable<Streamer> GetPreconfiguredStreamer()
        {
            return new List<Streamer>
            {
                new Streamer { CreatedBy = "fmonzon", Nombre = "Maxi HBP", Url = "https://www.hdp.com" },
                new Streamer { CreatedBy = "fmonzon", Nombre = "Amazon Vip", Url = "https://www.amazonvip.com" }
            };
        }
    }
}
