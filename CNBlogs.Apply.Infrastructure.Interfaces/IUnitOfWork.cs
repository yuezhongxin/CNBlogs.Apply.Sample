using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        void RegisterNew<TEntity>(TEntity entity)
            where TEntity : class;

        void RegisterDirty<TEntity>(TEntity entity)
            where TEntity : class;

        void RegisterClean<TEntity>(TEntity entity)
            where TEntity : class;

        void RegisterDeleted<TEntity>(TEntity entity)
            where TEntity : class;

        Task<bool> CommitAsync();

        void Rollback();
    }
}
