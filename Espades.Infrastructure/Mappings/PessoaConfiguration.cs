using Espades.Domain.Entities;
using Espades.Infrastructure.Mappings.BaseMappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Espades.Infrastructure.Mappings
{
    public class PessoaConfiguration : BaseEntityConfiguration<Pessoa>
    {
        public PessoaConfiguration(EntityTypeBuilder<Pessoa> entityBuilder)
            : base("Pessoa", entityBuilder)
        {
            entityBuilder.Property(t => t.Email)
                .HasMaxLength(100)
                .IsRequired();
            entityBuilder.Property(t => t.Telefone)
                .HasMaxLength(11);
            entityBuilder.Property(t => t.Sexo)
                .HasMaxLength(1)
                .IsRequired();
            entityBuilder.Property(t => t.CPF)
                .HasMaxLength(14)
                .IsRequired();
            entityBuilder.Property(t => t.Login)
                .HasMaxLength(50)
                .IsRequired();
            entityBuilder.Property(t => t.Senha)
                .HasMaxLength(600);
            entityBuilder.Property(t => t.Nome)
                .HasMaxLength(100);
        }
    }
}
