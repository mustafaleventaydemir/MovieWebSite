using _1AspApp.Entity;
using System.Collections.Generic;

namespace _1AspApp.Models
{
    public class MoviesViewModel
    {
        public List<Movie> Movies { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
