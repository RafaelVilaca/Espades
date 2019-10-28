using Espades.Api.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Espades.Api.Models
{
    public class SetorModel : BaseModel
    {
        public string Descricao { get; set; }
        [NotMapped]
        public List<CargoModel> Cargos { get; set; }
    }
}
