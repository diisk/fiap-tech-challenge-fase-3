using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contato.Infrastructure.Configurations
{
    public class ContatoConfiguration : IEntityTypeConfiguration<Domain.Entities.Contato>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Contato> builder)
        {
            builder.HasOne(c => c.Area)
                .WithMany()
                .HasForeignKey(c => c.CodigoArea)
                .HasPrincipalKey(a=>a.Codigo)
                .IsRequired();
        }
    }
}
