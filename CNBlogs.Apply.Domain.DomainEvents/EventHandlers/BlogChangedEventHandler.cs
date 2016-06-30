using CNBlogs.Apply.ServiceAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Domain.DomainEvents
{
    public class BlogChangedEventHandler :
        IEventHandler<BlogChangedEvent>
    {
        public async Task Handle(BlogChangedEvent @event)
        {
            ///to do...
        }
    }
}
