using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Domain.DomainEvents
{
    public interface IEventHandler<TEvent>
         where TEvent : IEvent
    {
        Task Handle(TEvent @event);
    }
}
