using CNBlogs.Apply.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Repository.Interfaces
{
    public interface IApplyRepository<TApplyAggregateRoot> : IRepository<TApplyAggregateRoot>
        where TApplyAggregateRoot : ApplyAggregateRoot
    {
        IQueryable<TApplyAggregateRoot> GetByUserId(int userId);

        IQueryable<TApplyAggregateRoot> GetWaiting(int userId);

        IQueryable<TApplyAggregateRoot> GetWaiting();
    }
}
