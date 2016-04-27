using CNBlogs.Apply.Infrastructure.Interfaces;
using CNBlogs.Apply.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNBlogs.Apply.Domain;
using CNBlogs.Apply.Domain.ValueObjects;

namespace CNBlogs.Apply.Repository
{
    public class JsPermissionApplyRepository : BaseRepository<JsPermissionApply>, IJsPermissionApplyRepository
    {
        public JsPermissionApplyRepository(IDbContext dbContext)
            : base(dbContext)
        { }

        public IQueryable<JsPermissionApply> GetInvalid(int userId)
        {
            return _entities.Where(x => x.User.Id == userId && x.Status != Status.Deny && x.IsActive);
        }

        public IQueryable<JsPermissionApply> GetWaiting(int userId)
        {
            return _entities.Where(x => x.User.Id == userId && x.Status == Status.Wait && x.IsActive);
        }

        public IQueryable<JsPermissionApply> GetWaiting()
        {
            return _entities.Where(x => x.Status == Status.Wait && x.IsActive);
        }
    }
}
