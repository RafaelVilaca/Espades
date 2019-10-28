using Espades.Domain.Entities;
using Espades.Infrastructure.Mappings.BaseMappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espades.Infrastructure.Mappings
{
    public class ReservaConfiguration : BaseEntityConfiguration<Reserva>
    {
        public ReservaConfiguration(EntityTypeBuilder<Reserva> entityBuilder)
            : base("Reserva", entityBuilder)
        {
            entityBuilder.Property(t => t.Data_Final_Reserva)
                .IsRequired();
            entityBuilder.Property(t => t.Quantidade)
                .IsRequired();
            entityBuilder.Property(t => t.Id_Cliente)
                .IsRequired();
            entityBuilder.Property(t => t.Id_Produto)
                .IsRequired();
            entityBuilder.HasIndex(t => t.Id_Cliente).ForSqlServerIsClustered(false);
            entityBuilder.HasIndex(t => t.Id_Produto).ForSqlServerIsClustered(false);
        }
    }
}
