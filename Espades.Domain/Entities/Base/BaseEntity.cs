using System.ComponentModel.DataAnnotations;

namespace Espades.Domain.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
}
