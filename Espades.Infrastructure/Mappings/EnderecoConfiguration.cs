using Espades.Domain.Entities;
using Espades.Infrastructure.Mappings.BaseMappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espades.Infrastructure.Mappings
{
    public class EnderecoConfiguration : BaseEntityConfiguration<Endereco>
    {
        public EnderecoConfiguration(EntityTypeBuilder<Endereco> entityBuilder)
            : base("Endereco", entityBuilder)
        {
            entityBuilder.Property(t => t.Rua)
                .HasMaxLength(100);
            entityBuilder.Property(t => t.CEP)
                .HasMaxLength(9);
            entityBuilder.Property(t => t.Numero);
            entityBuilder.Property(t => t.Complemento)
                .HasMaxLength(30);
            entityBuilder.Property(t => t.Cidade)
                .HasMaxLength(50);
            entityBuilder.Property(t => t.Estado)
                .HasMaxLength(2);
            entityBuilder.Property(t => t.Id_Pessoa);
            entityBuilder.HasIndex(t => t.Id_Pessoa).ForSqlServerIsClustered(false);
            entityBuilder.Property(t => t.Id_Cliente);
            entityBuilder.HasIndex(t => t.Id_Cliente).ForSqlServerIsClustered(false);
        }
    }
}
