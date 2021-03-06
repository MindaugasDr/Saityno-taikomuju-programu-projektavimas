using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data.Dtos.Users
{
    public record CreateUserDto([Required]string Name, [Required][Phone]string PhoneNumber, [Required]string Password);

}
