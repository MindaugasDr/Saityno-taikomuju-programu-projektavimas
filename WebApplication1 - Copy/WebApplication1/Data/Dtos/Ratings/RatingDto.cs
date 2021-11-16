using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data.Dtos.Ratings
{
    public record RatingDto(string User_id, string Reviewer_id, int Left_rating);

}
