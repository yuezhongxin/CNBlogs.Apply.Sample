using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Infrastructure.IoC.Contracts
{
    public class IocContainer
    {
        public static UnityContainer Default = new UnityContainer();
    }
}
