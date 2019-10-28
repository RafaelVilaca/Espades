using Espades.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Espades.Domain.Entities
{
    public class Funcionario : BaseEntity
    {
        public int Id_Pessoa { get; set; }
        public decimal? Salario { get; set; }
        public int Id_Cargo { get; set; }
        [NotMapped]
        public Cargo Cargo { get; set; }
        [NotMapped]
        public Pessoa Pessoa { get; set; }
    }
}
