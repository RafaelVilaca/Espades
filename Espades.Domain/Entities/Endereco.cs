using Espades.Domain.Entities.Base;

namespace Espades.Domain.Entities
{
    public class Endereco : BaseEntity
    {
        public string Rua { get; set; } //VARCHAR(100),
        public string CEP { get; set; } //VARCHAR(9), 
        public int? Numero { get; set; } //INT
        public string Complemento { get; set; } //VARCHAR(30) 
        public string Cidade { get; set; } //VARCHAR(50), 
        public string Estado { get; set; } //CHAR(2)
        public int? Id_Pessoa { get; set; } //INT,
    }
}
