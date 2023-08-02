using Movies.Data.Base;
using Movies.Models;

namespace Movies.Data.Services
{
    public class ProducersService : EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(AppDbContext Context) : base(Context)
        {

        }
    }
}
