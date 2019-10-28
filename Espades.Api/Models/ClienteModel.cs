using Espades.Api.Models.Base;

namespace Espades.Api.Models
{
    public class ClienteModel : BaseModel
    {
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public string Nome_Fantasia { get; set; }
    }
}
