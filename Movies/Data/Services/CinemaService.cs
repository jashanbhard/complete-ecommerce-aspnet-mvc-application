using Movies.Data.Base;
using Movies.Models;

namespace Movies.Data.Services
{
    public class CinemaService:EntityBaseRepository<Cinema>,ICinemaService
    {
        public CinemaService(AppDbContext _context) : base(_context)
        {

        }
    }
}
