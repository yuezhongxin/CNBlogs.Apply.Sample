using CNBlogs.Apply.Domain;
using CNBlogs.Apply.Domain.ValueObjects;
using CNBlogs.Apply.Infrastructure.Interfaces;
using CNBlogs.Apply.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Repository
{
    public abstract class ApplyRepository<TApplyAggregateRoot> : BaseRepository<TApplyAggregateRoot>, IApplyRepository<TApplyAggregateRoot>
        where TApplyAggregateRoot : ApplyAggregateRoot
    {
        public ApplyRepository(IDbContext dbContext)
            : base(dbContext)
        { }

        public IQueryable<TApplyAggregateRoot> GetByUserId(int userId)
        {
            return _entities.Where(x => x.User.Id == userId && x.IsActive);
        }

        public IQueryable<TApplyAggregateRoot> GetWaiting(int userId)
        {
            return _entities.Where(x => x.User.Id == userId && x.Status == Status.Wait && x.IsActive);
        }

        public IQueryable<TApplyAggregateRoot> GetWaiting()
        {
            return _entities.Where(x => x.Status == Status.Wait && x.IsActive);
        }
    }
}
