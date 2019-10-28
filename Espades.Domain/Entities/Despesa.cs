using Espades.Domain.Entities.Base;
using System;

namespace Espades.Domain.Entities
{
    public class Despesa : BaseEntity
    {
        public DateTime Data_Despesa { get; set; }
        public decimal Valor { get; set; }
        public string Local { get; set; }
        public string Descricao { get; set; }
    }
}
