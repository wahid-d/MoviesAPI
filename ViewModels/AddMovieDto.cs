using System;
using System.Collections.Generic;
using Movies.Models;

namespace Movies.ViewModels
{
    public class AddMovieViewModel
    {
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public double Imdb { get; set; }

        public string Description { get; set; }

        public List<Actor> Cast { get; set; }
    }
}
