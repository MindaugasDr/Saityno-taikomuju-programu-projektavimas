using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data.Dtos.Images
{
    public record EditImageDto([Required]string Image_url);
}
