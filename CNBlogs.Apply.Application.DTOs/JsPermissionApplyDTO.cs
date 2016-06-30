using CNBlogs.Apply.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Application.DTOs
{
    public class JsPermissionApplyDTO
    {
        public int Id { get; set; }

        public string Reason { get; set; }

        public string Ip { get; set; }

        public DateTime ApplyTime { get; set; }

        public int UserId { get; set; }

        public string UserDisplayName { get; set; }

        public string UserAlias { get; set; }

        public DateTime? UserRegisterTime { get; set; }
    }
}
