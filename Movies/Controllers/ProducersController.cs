using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;

namespace Movies.Controllers
{
    public class ProducersController : Controller
    {
        private readonly AppDbContext _context;
        public ProducersController(AppDbContext context)
        {
            _context=context;
        }
        public async Task<IActionResult> Index()
        {
            var allproducers = await _context.Producers.ToListAsync();
            return View(allproducers);
        }
    }
}
