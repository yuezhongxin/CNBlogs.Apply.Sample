using CNBlogs.Apply.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Repository.Interfaces
{
    public interface IBlogChangeApplyRepository : IApplyRepository<BlogChangeApply>
    {
        IQueryable<BlogChangeApply> GetByTargetAliasWithWait(string targetBlogApp);
    }
}
