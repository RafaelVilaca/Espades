using Espades.Api.Models.Base;
using System;

namespace Espades.Api.Models
{
    public class PatrimonioModel : BaseModel
    {
        public string Descricao { get; set; }
        public string Situacao { get; set; }
        public DateTime Data_Compra { get; set; }
        public decimal Valor { get; set; }
    }
}
