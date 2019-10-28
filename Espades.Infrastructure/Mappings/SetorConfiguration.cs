using Espades.Domain.Entities;
using Espades.Infrastructure.Mappings.BaseMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espades.Infrastructure.Mappings
{
    public class SetorConfiguration : BaseEntityConfiguration<Setor>
    {
        public SetorConfiguration(EntityTypeBuilder<Setor> entityBuilder)
            : base("Setor", entityBuilder)
        {
            entityBuilder.Property(t => t.Descricao)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
