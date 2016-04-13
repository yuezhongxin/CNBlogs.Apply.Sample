using CNBlogs.Apply.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Repository.Interfaces
{
    public interface IJsPermissionApplyRepository : IRepository<JsPermissionApply>
    {
        IQueryable<JsPermissionApply> GetByUserId(int userId);
    }
}
