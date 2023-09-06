using Movies.Data.Base;
using Movies.Data.ViewModels;
using Movies.Models;

namespace Movies.Data.Services
{
    public interface IMoviesService:IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsynch(int id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();
        Task AddnewMovieAsync(NewMovieVM data);
        Task UpdateMovieAsync(NewMovieVM data);
    }
}
