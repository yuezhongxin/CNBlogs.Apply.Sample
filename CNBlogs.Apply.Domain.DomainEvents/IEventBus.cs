using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Domain.DomainEvents
{
    public interface IEventBus
    {
        Task Publish<TEvent>(TEvent @event)
            where TEvent : IEvent;
    }
}
