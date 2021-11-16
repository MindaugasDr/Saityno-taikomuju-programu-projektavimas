﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data.Dtos.Events
{
        public record EditEventDto([Required][RegularExpression("([0-9]+)")] int CurrentSum);
}