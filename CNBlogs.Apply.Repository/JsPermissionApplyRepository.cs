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
    public class JsPermissionApplyRepository : ApplyRepository<JsPermissionApply>, IJsPermissionApplyRepository
    {
        public JsPermissionApplyRepository(IDbContext dbContext)
            : base(dbContext)
        { }
    }
}
