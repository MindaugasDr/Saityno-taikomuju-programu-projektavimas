using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data.Entities
{
    public class Image
    {
        [Key]
        public int Image_id { get; set; }
        public string Image_url { get; set; }
        public int Item_id { get; set; }
       // public Item item { get; set; }
    }
}
