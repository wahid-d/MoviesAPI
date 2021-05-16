using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Movies.Models;

namespace Movies.ViewModels
{
    public class ChangeMovieViewModel
    {
        [Required(ErrorMessage = "Max length is 10.")]
        [MaxLength(10)]
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public double Imdb { get; set; }

        public string Description { get; set; }

        public List<Actor> Cast { get; set; }
    }
}
