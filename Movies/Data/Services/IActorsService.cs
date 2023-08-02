using Movies.Data.Base;
using Movies.Models;
using System.Reflection.Metadata.Ecma335;

namespace Movies.Data.Services
{
    public interface IActorsService : IEntityBaseRepository<Actor>
    {
    }
}
