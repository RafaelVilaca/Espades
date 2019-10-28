using Espades.Domain.Entities;
using Espades.Infrastructure.Mappings.BaseMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espades.Infrastructure.Mappings
{
    public class DespesaConfiguration : BaseEntityConfiguration<Despesa>
    {
        public DespesaConfiguration(EntityTypeBuilder<Despesa> entityBuilder)
            : base("Despesa", entityBuilder)
        {
            entityBuilder.Property(t => t.Descricao)
                .HasMaxLength(100)
                .IsRequired();
            entityBuilder.Property(t => t.Data_Despesa)
                .IsRequired();
            entityBuilder.Property(t => t.Valor)
                .IsRequired();
            entityBuilder.Property(t => t.Local)
                .HasMaxLength(50);
        }
    }
}
