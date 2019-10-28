using Espades.Domain.Entities;
using Espades.Infrastructure.Mappings.BaseMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espades.Infrastructure.Mappings
{
    public class ProdutoConfiguration : BaseEntityConfiguration<Produto>
    {
        public ProdutoConfiguration(EntityTypeBuilder<Produto> entityBuilder)
            : base("Produto", entityBuilder)
        {
            entityBuilder.Property(t => t.Descricao)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
