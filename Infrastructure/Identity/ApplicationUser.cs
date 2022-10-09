using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string LikedCoursesId { get; set; }
        public string DislikedCoursesId { get; set; }
        [NotMapped]
        public int[] LikedCourses
        {
            get
            {
                return String.IsNullOrEmpty(LikedCoursesId)
                    ? Array.Empty<int>()
                    :Array.ConvertAll(LikedCoursesId.Split(';'), int.Parse);
            }
            set
            {
                
                LikedCoursesId = value.Length<1 || value== null
                    ?String.Empty
                    :String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }
        [NotMapped]
        public int[] DislikedCourses
        {
            get
            {
                return String.IsNullOrEmpty(DislikedCoursesId)
                    ?Array.Empty<int>()
                    :Array.ConvertAll(DislikedCoursesId.Split(';'), int.Parse);
            }
            set
            {

                DislikedCoursesId = value.Length <1 || value == null
                    ?String.Empty
                    :String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }

    }
}
