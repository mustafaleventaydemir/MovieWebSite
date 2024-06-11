using _1AspApp.Data;
using _1AspApp.Entity;
using _1AspApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace _1AspApp.Controllers
{
    //controller
    public class MoviesController : Controller
    {
        private readonly MovieContext _context;
        public MoviesController(MovieContext context)
        {
            _context = context;
        }
        //action
        //localhost:5000/movies/list
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //localhost:5000/movies/list
        //localhost:5000/movies/list/1
        [HttpGet]
        public IActionResult List(int? id, string q) //list'ten bir id parametresi gelmezse id==null olsun dedik
        {
            //{controller}/{action}/{id?}
            //movies/list/2

            //var controller = RouteData.Values["controller"];
            //var action = RouteData.Values["action"];
            //var genreid = RouteData.Values["id"];

            //var movies = MovieRepository.Movies;
            var movies = _context.Movies.AsQueryable();
            if (id != null)
            {
                movies = movies
                    .Include(m=>m.Genres)
                    .Where(m => m.Genres.Any(g=>g.GenreId == id));
            }

            if (!string.IsNullOrEmpty(q)) //null ya da boş olmadığı durumlarda
            {
                movies = movies.Where(i =>
                i.Title.ToLower().Contains(q.ToLower()) ||
                i.Description.ToLower().Contains(q.ToLower()));
            }



            var model = new MoviesViewModel()
            {                     //MovieRepository.Movie yerine
                Movies = movies.ToList() //bütün bilgileri aktarmaktansa
                                //filtrelediğimiz bilgileri aktardık

            };

            return View("Movies", model);
        }
        //localhost:5000/movies/details/1
        [HttpGet]  //get metodu çalışsın diye belirttik
        public IActionResult Details(int id)
        {
            return View(_context.Movies.Find(id));
        }

        [HttpGet]
        public IActionResult Create() //film ekleme ekranı tetikleyici get
        {
            ViewBag.Genres = new SelectList(_context.Genres.ToList(), "GenreId", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Movie m) //film ekleme ekranı tetikleyici post
        {
            if (ModelState.IsValid)
            {
                //MovieRepository.Add(m);
                _context.Movies.Add(m);
                _context.SaveChanges();
                TempData["Message"] = $"{m.Title} isimli film eklendi.";
                return RedirectToAction("List");
            }
            ViewBag.Genres = new SelectList(_context.Genres.ToList(), "GenreId", "Name");
            return View();

        }
        [HttpGet]

        public IActionResult Edit(int id)
        {
            ViewBag.Genres = new SelectList(_context.Genres.ToList(), "GenreId", "Name");
            return View(_context.Movies.Find(id));
        }
        [HttpPost]
        public IActionResult Edit(Movie m)
        {
            if (ModelState.IsValid)
            {
                //MovieRepository.Edit(m);
                // /movies/details/1
                _context.Movies.Update(m);
                _context.SaveChanges();
                TempData["Message"] = $"{m.Title} isimli film güncellendi.";
                return RedirectToAction("Details", "Movies", new { @id = m.MovieId });
            }
            ViewBag.Genres = new SelectList(_context.Genres.ToList(), "GenreId", "Name");
            return View(m);
        }
        public IActionResult Delete(int MovieId, string Title)
        {
            //MovieRepository.Delete(MovieId);
            var entity = _context.Movies.Find(MovieId);
            _context.Movies.Remove(entity);
            _context.SaveChanges();
            TempData["Message"] = $"{Title} isimli film silindi.";
            return RedirectToAction("List");
        }
    }
}
