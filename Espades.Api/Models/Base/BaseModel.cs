using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Espades.Api.Models.Base
{
    public class BaseModel
    {
        public long Id { get; set; }
        public bool Deleted { get; set; }
    }
}
