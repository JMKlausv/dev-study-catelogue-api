using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Framework : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }    
        public Language language { get; set; }
        public int LanguageId { get; set; }
    }
}
