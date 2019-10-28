using Espades.Domain.Entities;
using Espades.Infrastructure.Mappings.BaseMappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espades.Infrastructure.Mappings
{
    public class CargoConfiguration : BaseEntityConfiguration<Cargo>
    {
        public CargoConfiguration(EntityTypeBuilder<Cargo> entityBuilder)
            : base("Cargo", entityBuilder)
        {
            entityBuilder.Property(t => t.Descricao)
                .HasMaxLength(50);
            entityBuilder.Property(t => t.Id_Setor);
            entityBuilder.HasIndex(t => t.Id_Setor).ForSqlServerIsClustered(false);
        }
    }
}
