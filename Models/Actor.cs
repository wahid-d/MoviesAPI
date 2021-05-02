using System;

namespace Movies.Models
{
    public class Actor : EntityBase
    {
        public string FullName { get; set; }

        public DateTime Bithdate { get; set; }

        public string ImageUrl { get; set; }
    }
}
