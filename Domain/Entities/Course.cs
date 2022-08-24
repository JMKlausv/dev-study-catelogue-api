using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Course :BaseAuditableEntity
    {
        public string Title { get; set; }    
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }    
        public string ContentLink { get; set; }
        public DateTime PublishedDate { get; set; }
        public Framework Framework { get; set; }
        public int FrameworkId { get; set; }
        public string Difficulty { get; set; }  
        public string PlatformType { get; set; }    
        public int UpvoteCount { get; set; } = 0;   
        public int DownvoteCount { get; set; }  = 0;  
        public string UploadedBy { get; set; }  
        public string Division { get; set; }


    }
}
