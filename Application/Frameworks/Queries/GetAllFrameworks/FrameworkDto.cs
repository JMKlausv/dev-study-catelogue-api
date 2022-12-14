using Application.Common.Mappings;
using Domain.Entities;


namespace Application.Frameworks.Queries.GetAllFrameworks
{
    public class FrameworkDto : IMapFrom<Framework>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public FrameWorkLanguageDto Language { get; set; }  
    }
}
