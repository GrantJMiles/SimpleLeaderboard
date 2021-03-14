using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SimpleLeaderboard.Domain
{
    public class CreateEventDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}