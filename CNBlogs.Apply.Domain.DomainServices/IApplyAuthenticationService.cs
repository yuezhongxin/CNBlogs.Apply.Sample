using CNBlogs.Apply.Domain.ValueObjects;
using CNBlogs.Apply.Repository.Interfaces;
using CNBlogs.Apply.ServiceAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Domain.DomainServices
{
    public interface IApplyAuthenticationService
    {
        Task<string> VerfiyForJsPermission(User user);

        Task<string> VerfiyForBlogChange(User user, string targetBlogApp);
    }
}
