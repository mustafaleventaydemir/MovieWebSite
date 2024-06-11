using _1AspApp.Data;
using _1AspApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace _1AspApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieContext _context;
        public HomeController(MovieContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = new HomePageViewModel()
            {
                PopularMovies = _context.Movies.ToList()
            };



            return View(model);
        }

        public IActionResult About()
        {
         
            return View();
        }
    }
}
