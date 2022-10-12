using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class UserPreference
    {
        public int[] LikedCourses { get; set; }
        public int[] DislikedCourses { get; set; }  
    }
}
