using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Frameworks.Queries.GetAllFrameworks
{
    public class FrameworkDto : IMapFrom<Framework>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int LanguageId { get; set; }
    }
}
