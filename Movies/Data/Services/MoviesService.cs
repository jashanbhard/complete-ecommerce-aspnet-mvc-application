using Microsoft.EntityFrameworkCore;
using Movies.Data.Base;
using Movies.Data.ViewModels;
using Movies.Models;

namespace Movies.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddnewMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId,
                CinemaId = data.CinemaId,
            };
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();


            //Adding movie actors
            foreach (var actorid in data.ActorIds)
            {
                var newMovieactor = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorid
                };
                await _context.Actors_Movies.AddAsync(newMovieactor);
            }
            await _context.SaveChangesAsync();
        }


        public async Task<Movie> GetMovieByIdAsynch(int id)
        {
            var movieDetails = await _context.Movies
           .Include(c => c.Cinema)
           .Include(p => p.Producer)
           .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
           .FirstOrDefaultAsync(n => n.Id == id);
            return movieDetails;
        }
        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };
            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);
            if (dbMovie != null)
            {
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;
                dbMovie.CinemaId = data.CinemaId;
                
                await _context.SaveChangesAsync();
            }

            //Remove Existing Actors
            var existingActorDb = _context.Actors_Movies.Where(n=>n.MovieId == data.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorDb);
            await _context.SaveChangesAsync();

            //Adding movie actors
            foreach (var actorid in data.ActorIds)
            {
                var newMovieactor = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorid
                };
                await _context.Actors_Movies.AddAsync(newMovieactor);
            }
            await _context.SaveChangesAsync();
        }
    }
}
