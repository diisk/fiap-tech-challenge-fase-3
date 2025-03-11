using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Area.Infrastructure.Configurations
{
    public class AreaConfiguration : IEntityTypeConfiguration<Domain.Entities.Area>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Area> builder)
        {
            builder.HasIndex(a=>a.Codigo);
        }
    }
}
