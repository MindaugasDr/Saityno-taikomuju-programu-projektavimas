using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data.Dtos.Items
{
public record ItemDto(int Item_id, string Item_name, string Item_description, DateTime CreationDate, string User_id);
}
