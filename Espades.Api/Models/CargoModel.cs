using Espades.Api.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Espades.Api.Models
{
    public class CargoModel : BaseModel
    {
        public string Descricao { get; set; }
        public int Id_Setor { get; set; }
        [NotMapped]
        public SetorModel Setor { get; set; }
    }
}
