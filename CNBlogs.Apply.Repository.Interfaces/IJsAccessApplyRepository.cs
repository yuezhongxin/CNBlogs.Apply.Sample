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
        IQueryable<JsPermissionApply> GetInvalid(int userId);

        IQueryable<JsPermissionApply> GetWaiting(int userId);

        IQueryable<JsPermissionApply> GetWaiting();
    }
}
