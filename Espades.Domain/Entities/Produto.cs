using Espades.Domain.Entities.Base;

namespace Espades.Domain.Entities
{
    public class Produto : BaseEntity
    {
        public string Descricao { get; set; }
        //Descricao VARCHAR(100) NOT NULL
    }
}