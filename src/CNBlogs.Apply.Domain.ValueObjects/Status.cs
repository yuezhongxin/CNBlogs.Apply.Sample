using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Domain.ValueObjects
{
    public enum Status
    {
        NoApply = 0,

        Wait = 1,

        Pass = 2,

        Deny = 3,

        Lock = 4,
    }
}
