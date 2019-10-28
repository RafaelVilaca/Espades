using Espades.Api.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Espades.Api.Models
{
    public class PessoaModel : BaseModel
    {
        public string Nome { get; set; }
        public string Email { get; set; } //VARCHAR(100) NOT NULL,
        public string Telefone { get; set; } //VARCHAR(11), 
        public string Sexo { get; set; } //CHAR(1) NOT NULL,
        public string CPF { get; set; } //VARCHAR(14) NOT NULL,
        public string Login { get; set; } //VARCHAR(50) NOT NULL,
        public string Senha { get; set; } //VARCHAR(600),
        [NotMapped]
        public List<EnderecoModel> Enderecos { get; set; }
    }
}
