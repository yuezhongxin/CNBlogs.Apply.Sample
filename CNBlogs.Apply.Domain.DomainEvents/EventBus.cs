using CNBlogs.Apply.Infrastructure.IoC.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace CNBlogs.Apply.Domain.DomainEvents
{
    public class EventBus : IEventBus
    {
        public async Task Publish<TEvent>(TEvent @event)
            where TEvent : IEvent
        {
            var eventHandler = IocContainer.Default.Resolve<IEventHandler<TEvent>>();
            await eventHandler.Handle(@event);
        }
    }
}
