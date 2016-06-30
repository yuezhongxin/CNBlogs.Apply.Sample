using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Domain.DomainEvents
{
    public class JsPermissionOpenedEvent : IEvent
    {
        public string UserAlias { get; set; }
    }
}
