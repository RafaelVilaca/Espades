using Espades.Domain.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Espades.Domain.Entities
{
    public class Estoque : BaseEntity
    {
        public int Id_Produto { get; set; }
        public string Localizacao { get; set; }
        public string Descricao { get; set; }
        public DateTime Data_Compra { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Valor { get; set; }
        [NotMapped]
        public Produto Produto { get; set; }
    }
}
