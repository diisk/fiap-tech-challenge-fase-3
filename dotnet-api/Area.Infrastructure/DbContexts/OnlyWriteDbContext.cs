using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Area.Infrastructure.DbContexts
{
    public class OnlyWriteDbContext: DbContext
    {
        public OnlyWriteDbContext(DbContextOptions<OnlyWriteDbContext> options):base(options) {}
        public DbSet<Domain.Entities.Area> AreaSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnlyReadDbContext).Assembly);
        }
    }
}
