using Espades.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Espades.Domain.Entities
{
    public class Setor : BaseEntity
    {
        public string Descricao { get; set; }
        [NotMapped]
        public List<Cargo> Cargos { get; set; }
        //Descricao VARCHAR(100) NOT NULL
    }
}