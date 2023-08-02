using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using Microsoft.EntityFrameworkCore;

namespace Movies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;
        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task <IActionResult> Index()
        {
            var allmovies = await _context.Movies.Include(k=>k.Cinema).OrderBy(n=>n.Name).ToListAsync();
            return View(allmovies);
        }
    }
}
