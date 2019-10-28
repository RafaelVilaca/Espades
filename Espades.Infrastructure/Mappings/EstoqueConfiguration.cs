using Espades.Domain.Entities;
using Espades.Infrastructure.Mappings.BaseMappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espades.Infrastructure.Mappings
{
    public class EstoqueConfiguration : BaseEntityConfiguration<Estoque>
    {
        public EstoqueConfiguration(EntityTypeBuilder<Estoque> entityBuilder)
            : base("Estoque", entityBuilder)
        {
            entityBuilder.Property(t => t.Descricao)
                .HasMaxLength(100);
            entityBuilder.Property(t => t.Localizacao)
                .HasMaxLength(5)
                .IsRequired();
            entityBuilder.Property(t => t.Data_Compra)
                .IsRequired();
            entityBuilder.Property(t => t.Quantidade)
                .IsRequired();
            entityBuilder.Property(t => t.Valor)
                .IsRequired();
            entityBuilder.Property(t => t.Id_Produto);
            entityBuilder.HasIndex(t => t.Id_Produto).ForSqlServerIsClustered(false);
            //Quantidade DECIMAL(10,2) NOT NULL,
            //   Valor DECIMAL(10, 2) NOT NULL,
            //   ID_Reserva INT NOT NULL,
            //Deleted BIT DEFAULT 0,
        }
    }
}
