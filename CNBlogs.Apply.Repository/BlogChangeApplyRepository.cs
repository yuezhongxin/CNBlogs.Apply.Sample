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
    public class BlogChangeApplyRepository : ApplyRepository<BlogChangeApply>, IBlogChangeApplyRepository
    {
        public BlogChangeApplyRepository(IDbContext dbContext)
            : base(dbContext)
        { }

        public IQueryable<BlogChangeApply> GetByTargetAliasWithWait(string targetBlogApp)
        {
            return _entities.Where(x => x.TargetBlogApp == targetBlogApp && x.Status == Status.Wait && x.IsActive);
        }
    }
}
