using Espades.Domain.Entities.Base;

namespace Espades.Domain.Entities
{
    public class Cliente : BaseEntity
    {
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public string Nome_Fantasia { get; set; }
    }
}
