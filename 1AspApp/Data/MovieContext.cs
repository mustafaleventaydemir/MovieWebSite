using _1AspApp.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace _1AspApp.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<Cast> Casts { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=movies.db");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder) //bu metotla veritabanındaki default kriterleri ezdik
        {
            //fluent api kullanarak tablolardaki kolonlara kısıtlayıcı özellikleri bu şekilde override metot tanımlayarak atayabiliriz.
            //ve en son bunu migration olarak eklememiz gerekir
            modelBuilder.Entity<Movie>().Property(b => b.Title).IsRequired();
            modelBuilder.Entity<Movie>().Property(b => b.Title).HasMaxLength(500);

            modelBuilder.Entity<Genre>().Property(b => b.Name).IsRequired();
            modelBuilder.Entity<Genre>().Property(b => b.Name).HasMaxLength(50);
        }
    }
}
