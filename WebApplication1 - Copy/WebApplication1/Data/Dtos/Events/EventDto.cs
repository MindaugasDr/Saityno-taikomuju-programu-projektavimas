using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data.Dtos.Events
{
    public record EventDto(int Event_id, string Action, DateTime Date, int CurrentSum, string User_id, int Item_id);
}
