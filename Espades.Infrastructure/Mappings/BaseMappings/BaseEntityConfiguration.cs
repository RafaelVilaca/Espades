using Espades.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espades.Infrastructure.Mappings.BaseMappings
{
    public abstract class BaseEntityConfiguration<T>
        where T : BaseEntity
    {
        public BaseEntityConfiguration(string tableName, EntityTypeBuilder<T> entityBuilder)
        {
            entityBuilder.ToTable(tableName);
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Deleted).IsRequired();
            entityBuilder.HasIndex(t => t.Deleted).ForSqlServerIsClustered(false);
        }
    }
}
