using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContext : DbContext
    {
        public StreamerDbContext(DbContextOptions<StreamerDbContext> options) : base(options)
        {
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=.; 
        //                                    Initial Catalog=Streamer; 
        //                                    Integrated Security=True")
        //        .LogTo(Console.WriteLine, new [] { DbLoggerCategory.Database.Command.Name }, 
        //                                           Microsoft.Extensions.Logging.LogLevel.Information)
        //        .EnableSensitiveDataLogging();
        //}

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    case EntityState.Modified:
                        entry.Entity.DateUpdated = DateTime.Now;
                        entry.Entity.ModifiedBy = "System";
                        break;
                    case EntityState.Added:
                        entry.Entity.DateCreated = DateTime.Now;
                        entry.Entity.CreatedBy = "System";
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Configuracion FLUENT API 1:M

            modelBuilder.Entity<Streamer>()
                .HasMany(x => x.Videos)
                .WithOne(x => x.Streamer)
                .HasForeignKey(x => x.StreamerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Configuracion FLUENT API M:M

            modelBuilder.Entity<Video>()
               .HasMany(x => x.Actores)
               .WithMany(z => z.Videos)
               .UsingEntity<VideoActor>
               (
                   xz => xz.HasKey(e => new { e.ActorId, e.VideoId })
               );

            #endregion
        }

        public DbSet<Streamer>? Streamers { get; set; }
        public DbSet<Video>? Videos { get; set; }
        public DbSet<Director>? Directores { get; set; }
        public DbSet<Actor>? Actores { get; set; }
        public DbSet<VideoActor>? VideoActores { get; set; }
    }
}
