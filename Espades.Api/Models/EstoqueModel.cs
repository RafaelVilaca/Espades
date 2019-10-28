using Espades.Api.Models.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Espades.Api.Models
{
    public class EstoqueModel : BaseModel
    {
        public int Id_Produto { get; set; }
        public string Localizacao { get; set; }
        public string Descricao { get; set; }
        public DateTime Data_Compra { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Valor { get; set; }
        [NotMapped]
        public ProdutoModel Produto { get; set; }
    }
}
