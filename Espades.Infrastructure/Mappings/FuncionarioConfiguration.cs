using Espades.Domain.Entities;
using Espades.Infrastructure.Mappings.BaseMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Espades.Infrastructure.Mappings
{
    public class FuncionarioConfiguration : BaseEntityConfiguration<Funcionario>
    {
        public FuncionarioConfiguration(EntityTypeBuilder<Funcionario> entityBuilder)
            : base("Funcionario", entityBuilder)
        {
            entityBuilder.Property(t => t.Id_Pessoa)
                .IsRequired();
            entityBuilder.Property(t => t.Id_Cargo)
                .IsRequired();
            entityBuilder.Property(t => t.Salario);
        }
    }
}
