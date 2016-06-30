using CNBlogs.Apply.ServiceAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Domain.DomainEvents
{
    public class JsPermissionOpenedEventHandler :
        IEventHandler<JsPermissionOpenedEvent>
    {
        public async Task Handle(JsPermissionOpenedEvent @event)
        {
            await BlogService.EnableJsPermission(@event.UserAlias);
        }
    }
}
