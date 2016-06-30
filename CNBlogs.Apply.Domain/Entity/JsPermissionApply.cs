using CNBlogs.Apply.Domain.DomainEvents;
using CNBlogs.Apply.Domain.ValueObjects;
using CNBlogs.Apply.ServiceAgent;
using CNBlogs.Apply.Infrastructure.IoC.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace CNBlogs.Apply.Domain
{
    public class JsPermissionApply : ApplyAggregateRoot
    {
        public JsPermissionApply()
        { }

        public JsPermissionApply(string reason, User user, string ip)
            : base(reason, user, ip)
        {
           
        }

        public async Task<Status> GetStatus(string userAlias)
        {
            if (await BlogService.HaveJsPermission(userAlias))
            {
                return Status.Pass;
            }
            else
            {
                if (this.Status == Status.Deny && DateTime.Now > this.ApplyTime.AddDays(3))
                {
                    return Status.None;
                }
                if (this.Status == Status.Pass)
                {
                    return Status.None;
                }
                return this.Status;
            }
        }

        public async Task<bool> Pass()
        {
            var replyContent = "恭喜您！您的JS权限申请已通过审批。";
            return await base.Pass(replyContent, new JsPermissionOpenedEvent() { UserAlias = this.User.Alias });
        }

        public bool Lock()
        {
            var replyContent = "抱歉！您的JS权限申请没有被批准，并且申请已被锁定，具体请联系contact@cnblogs.com。";
            return base.Lock(replyContent);
        }

        public async Task Passed()
        {
            await base.Passed("您的JS权限申请已批准");
        }

        public async Task Denied()
        {
            await base.Passed("您的JS权限申请未通过审批");
        }

        public async Task Locked()
        {
            await Denied();
        }
    }
}
