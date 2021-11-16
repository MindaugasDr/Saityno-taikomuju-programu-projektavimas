using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data.Dtos.Events
{
    public record CreateEventDto([Required] string Action, [Required][RegularExpression("([0-9]+)")] int CurrentSum,[Required]int Item_id);

}
