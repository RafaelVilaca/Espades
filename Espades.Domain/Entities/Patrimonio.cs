using Espades.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Espades.Domain.Entities
{
    public class Patrimonio : BaseEntity
    {
        public string Descricao { get; set; }
        public string Situacao { get; set; }
        public DateTime Data_Compra { get; set; }
        public decimal Valor { get; set; }
    }
}
