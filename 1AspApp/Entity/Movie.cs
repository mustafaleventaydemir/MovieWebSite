using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace _1AspApp.Entity
{
    public class Movie
    {
        //[Key,DatabaseGenerated(DatabaseGeneratedOption.None)] //kendim id atamak istersem 
        public int MovieId { get; set; }
       // [Required]
        public string Title { get; set; }
       // [MaxLength(500)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
       // [Required]
        //public Genre Genre { get; set; } //navigation property
        //public int GenreId { get; set; } //null olabilir int?

        public List<Genre> Genres { get; set; }
    }
}
