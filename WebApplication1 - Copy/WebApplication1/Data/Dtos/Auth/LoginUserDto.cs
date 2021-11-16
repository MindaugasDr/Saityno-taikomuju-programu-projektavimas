using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BidToBuy.Data.Dtos.Auth
{
    public record LoginUserDto([Required] string UserName, [Required] string Password);
}
