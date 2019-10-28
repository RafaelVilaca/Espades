using Espades.Api.Models.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Espades.Api.Models
{
    public class ReservaModel : BaseModel
    {
        public int Id_Cliente { get; set; }
        public int Id_Produto { get; set; }
        public DateTime? Data_Final_Reserva { get; set; }
        public decimal Quantidade { get; set; }
        [NotMapped]
        public ProdutoModel Produto { get; set; }
        [NotMapped]
        public ClienteModel Cliente { get; set; }
    }
}
