using Espades.Api.Models.Base;
using System;

namespace Espades.Api.Models
{
    public class DespesaModel : BaseModel
    {
        public DateTime Data_Despesa { get; set; }
        public decimal Valor { get; set; }
        public string Local { get; set; }
        public string Descricao { get; set; }
    }
}
