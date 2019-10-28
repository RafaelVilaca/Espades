using Espades.Domain.Entities;
using Espades.Infrastructure.Mappings.BaseMappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espades.Infrastructure.Mappings
{
    public class ClienteConfiguration : BaseEntityConfiguration<Cliente>
    {
        public ClienteConfiguration(EntityTypeBuilder<Cliente> entityBuilder)
            : base("Cliente", entityBuilder)
        {
            entityBuilder.Property(t => t.CNPJ)
                .HasMaxLength(20)
                .IsRequired();
            entityBuilder.Property(t => t.Nome)
                .HasMaxLength(100)
                .IsRequired();
            entityBuilder.Property(t => t.Nome_Fantasia)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
