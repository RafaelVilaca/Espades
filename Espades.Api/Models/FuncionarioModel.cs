using Espades.Api.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Espades.Api.Models
{
    public class FuncionarioModel : BaseModel
    {
        public int Id_Pessoa { get; set; }
        public decimal? Salario { get; set; }
        public int Id_Cargo { get; set; }
        [NotMapped]
        public CargoModel Cargo { get; set; }
        [NotMapped]
        public PessoaModel Pessoa { get; set; }
    }
}
