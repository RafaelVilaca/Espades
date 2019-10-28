using Espades.Domain.Entities;
using Espades.Infrastructure.Mappings.BaseMappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espades.Infrastructure.Mappings
{
    public class PatrimonioConfiguration : BaseEntityConfiguration<Patrimonio>
    {
        public PatrimonioConfiguration(EntityTypeBuilder<Patrimonio> entityBuilder)
            : base("Patrimonio", entityBuilder)
        {
            entityBuilder.Property(t => t.Descricao)
                .HasMaxLength(100)
                .IsRequired();
            entityBuilder.Property(t => t.Situacao)
                .HasMaxLength(15)
                .IsRequired();
            entityBuilder.Property(t => t.Data_Compra);
            entityBuilder.Property(t => t.Valor);
        }
    }
}
