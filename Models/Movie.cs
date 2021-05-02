using System;
using System.Collections.Generic;

namespace Movies.Models
{
    public class Movie : EntityBase
    {
        public Movie(string title, DateTime releaseDate, double imdb, string description)
        {
            Title = title;
            ReleaseDate = releaseDate;
            Imdb = imdb;
            Description = description;
        }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public double Imdb { get; set; }

        public string Description { get; set; }

        public List<Actor> Cast { get; set; }

    }
}
