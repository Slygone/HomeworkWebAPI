using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Domain.Models;

namespace Workshop.DataAccess.Interface
{
    public interface IMovieRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
    }
}
