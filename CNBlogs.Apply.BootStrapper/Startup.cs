using CNBlogs.Apply.Infrastructure.IoC.Contracts;
using CNBlogs.Infrastructure.IoC.Unity;
using CNBlogs.Apply.Application.Interfaces;
using CNBlogs.Apply.Application.Services;
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
using CNBlogs.Apply.Domain;
using AutoMapper;
using CNBlogs.Apply.Application.DTOs;
using Microsoft.Practices.Unity;

namespace CNBlogs.Apply.BootStrapper
{
    public class Startup
    {
        public static void Configure()
        {
            UnityContainer register = IocContainer.Default;

            register.RegisterType<IUnitOfWork, UnitOfWork>();
            register.RegisterType<IDbContext, ApplyDbContext>(new PerThreadLifetimeManager());

            register.RegisterType<IJsPermissionApplyRepository, JsPermissionApplyRepository>();
            register.RegisterType<IBlogChangeApplyRepository, BlogChangeApplyRepository>();

            register.RegisterType<IJsPermissionApplyService, JsPermissionApplyService>();
            register.RegisterType<IBlogChangeApplyService, BlogChangeApplyService>();

            register.RegisterType<IEventBus, EventBus>();
            register.RegisterType<IEventHandler<MessageSentEvent>, MessageSentEventHandler>();
            register.RegisterType<IEventHandler<JsPermissionOpenedEvent>, JsPermissionOpenedEventHandler>();
            register.RegisterType<IEventHandler<BlogChangedEvent>, BlogChangedEventHandler>();

            register.RegisterType<IApplyAuthenticationService, ApplyAuthenticationService>();

            ConfigureMapper();
        }

        public static void ConfigureMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<JsPermissionApply, JsPermissionApplyDTO>();
                cfg.CreateMap<BlogChangeApply, BlogChangeApplyDTO>();
            });
        }
    }
}
