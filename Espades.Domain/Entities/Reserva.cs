using Espades.Domain.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Espades.Domain.Entities
{
    public class Reserva : BaseEntity
    {
        public int Id_Cliente { get; set; }
        public int Id_Produto { get; set; }
        public decimal Quantidade { get; set; }
        public DateTime? Data_Final_Reserva { get; set; }
        [NotMapped]
        public Produto Produto { get; set; }
        [NotMapped]
        public Cliente Cliente { get; set; }
    }
}
