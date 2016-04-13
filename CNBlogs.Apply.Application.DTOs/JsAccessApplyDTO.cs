using CNBlogs.Apply.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Application.DTOs
{
    public class JsPermissionApply
    {
        public int Id { get; set; }

        public string Reason { get; set; }

        public string Email { get; set; }

        public int UserId { get; set; }

        public string DisplyName { get; set; }

        public Status Status { get; set; }

        public string Ip { get; set; }

        public DateTime ApplyTime { get; set; } = DateTime.Now;

        public string ReplyContent { get; set; }

        public DateTime ApprovedTime { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;
    }
}
