using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data.Dtos.Items
{

 public record CreateItemDto([Required] string Item_name,[Required]string Item_description);

    
}
