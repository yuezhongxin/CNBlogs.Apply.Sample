using CNBlogs.Apply.Infrastructure;
using CNBlogs.Apply.Infrastructure.Interfaces;
using CNBlogs.Apply.Repository;
using CNBlogs.Apply.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNBlogs.Apply.Domain.DomainEvents;
using CNBlogs.Apply.Domain.DomainServices;
using CNBlogs.Apply.Infrastructure.IoC.Contracts;
using Microsoft.Practices.Unity;

namespace CNBlogs.Apply.BootStrapper
{
    public class Startup
    {
        public static void Configure()
        {
            UnityContainer container = IocContainer.Default;

            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IDbContext, ApplyDbContext>(new PerThreadLifetimeManager());

            container.RegisterType<IJsPermissionApplyRepository, JsPermissionApplyRepository>();
            //register.RegisterType<IJsPermissionApplyService, JsPermissionApplyService>();

            container.RegisterType<IEventBus, EventBus>();
            container.RegisterType<IEventHandler<MessageSentEvent>, MessageSentEventHandler>();
            container.RegisterType<IEventHandler<JsPermissionOpenedEvent>, JsPermissionOpenedEventHandler>();

            container.RegisterType<IApplyAuthenticationService, ApplyAuthenticationService>();
        }
    }
}
