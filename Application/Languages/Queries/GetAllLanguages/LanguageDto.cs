using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Languages.Queries.GetAllLanguages
{
    public class LanguageDto : IMapFrom<Language>
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
    }
}
