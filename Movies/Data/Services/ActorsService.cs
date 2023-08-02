using Microsoft.EntityFrameworkCore;
using Movies.Data.Base;
using Movies.Models;

namespace Movies.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(AppDbContext context) : base(context)
        {

        }
    }
}

