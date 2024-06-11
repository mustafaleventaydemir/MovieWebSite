using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _1AspApp.Entity
{
    public class Genre
    {
        public int GenreId { get; set; }
       // [Required]
        public string Name { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
