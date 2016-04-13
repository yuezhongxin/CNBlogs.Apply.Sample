using CNBlogs.Apply.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Repository.Interfaces
{
    public interface IRepository<TAggregateRoot> 
        where TAggregateRoot : class, IAggregateRoot
    {
        IQueryable<TAggregateRoot> Get(int id);

        IQueryable<TAggregateRoot> GetAll();
    }
}
