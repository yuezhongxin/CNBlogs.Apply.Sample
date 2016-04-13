using CNBlogs.Apply.Infrastructure.Interfaces;
using CNBlogs.Apply.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNBlogs.Apply.Domain;

namespace CNBlogs.Apply.Repository
{
    public class JsPermissionApplyRepository : BaseRepository<JsPermissionApply>, IJsPermissionApplyRepository
    {
        public JsPermissionApplyRepository(IDbContext dbContext)
            : base(dbContext)
        { }

        public IQueryable<JsPermissionApply> GetByUserId(int userId)
        {
            return _entities.Where(x => x.UserId == userId);
        }
    }
}
