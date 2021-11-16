using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data.Entities
{
    public class Rating
    {
        [Key]
        public int Rating_id { get; set; }
        public string User_id { get; set; }
        public string Reviewer_id { get; set; }
        public int Left_rating { get; set; }
    }
}
