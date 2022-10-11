using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class LoginResponse
    {
        public Result Result { get; set; }
        public string TokenString { get; set; }
        public string LikedCoursses { get; set; }
        public string DislikedCourses { get; set; }
    }
}
