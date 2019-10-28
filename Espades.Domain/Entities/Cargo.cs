using Espades.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Espades.Domain.Entities
{
    public class Cargo : BaseEntity
    {
        public string Descricao { get; set; }
        public int Id_Setor { get; set; }
        [NotMapped]
        public Setor Setor { get; set; }
    }
}
