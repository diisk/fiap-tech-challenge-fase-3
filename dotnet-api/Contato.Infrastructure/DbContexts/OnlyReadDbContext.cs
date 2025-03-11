using Microsoft.EntityFrameworkCore;

namespace Contato.Infrastructure.DbContexts
{
    public class OnlyReadDbContext: DbContext
    {
        public OnlyReadDbContext(DbContextOptions<OnlyReadDbContext> options):base(options) {}
        public DbSet<Domain.Entities.Contato> ContatoSet { get; set; }
        public DbSet<Domain.Entities.Area> AreaSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnlyReadDbContext).Assembly);
        }
    }
}
