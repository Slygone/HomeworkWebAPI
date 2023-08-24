using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.DataAccess.Interface;
using Workshop.Domain.Models;

namespace Workshop.DataAccess.Repository
{
    public class MovieRepository : IMovieRepository<Movie>
    {
        public List<Movie> GetAll()
        {
            return StaticDb.Movies;
        }
    }
}
