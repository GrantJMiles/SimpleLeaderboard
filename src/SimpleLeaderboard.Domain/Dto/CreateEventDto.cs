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
        public string UniqueId { get; set; }
        public string AdminId { get; set; }
        public bool IsDescending { get; set; } = true;
    }
}