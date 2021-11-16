using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data.Dtos.Ratings
{
    public record CreateRatingDto([Required] string Reviewer_id,[Required] int Left_rating);
}
