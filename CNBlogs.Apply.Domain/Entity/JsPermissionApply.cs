using CNBlogs.Apply.Domain.DomainEvents;
using CNBlogs.Apply.Domain.ValueObjects;
using CNBlogs.Apply.Infrastructure.IoC.Contracts;
using CNBlogs.Apply.ServiceAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Web;

namespace CNBlogs.Apply.Domain
{
    public class JsPermissionApply : IAggregateRoot
    {
        private IEventBus eventBus;

        public JsPermissionApply()
        { }

        public JsPermissionApply(string reason, User user, string ip)
        {
            if (string.IsNullOrEmpty(reason))
            {
                throw new ArgumentException("申请内容不能为空");
            }
            if (reason.Length > 3000)
            {
                throw new ArgumentException("申请内容超出最大长度");
            }
            if (user == null)
            {
                throw new ArgumentException("用户为null");
            }
            if (user.Id == 0)
            {
                throw new ArgumentException("用户Id为0");
            }
            this.Reason = HttpUtility.HtmlEncode(reason);
            this.User = user;
            this.Ip = ip;
            this.Status = Status.Wait;
        }

        public int Id { get; private set; }

        public string Reason { get; private set; }

        public virtual User User { get; private set; }

        public Status Status { get; private set; } = Status.Wait;

        public string Ip { get; private set; }

        public DateTime ApplyTime { get; private set; } = DateTime.Now;

        public string ReplyContent { get; private set; }

        public DateTime? ApprovedTime { get; private set; }

        public bool IsActive { get; private set; } = true;

        public async Task<bool> Pass()
        {
            if (this.Status != Status.Wait)
            {
                return false;
            }
            this.Status = Status.Pass;
            this.ApprovedTime = DateTime.Now;
            this.ReplyContent = "恭喜您！您的JS权限申请已通过审批。";
            eventBus = IocContainer.Default.Resolve<IEventBus>();
            await eventBus.Publish(new JsPermissionOpenedEvent() { UserId = this.User.Id });
            return true;
        }

        public bool Deny(string replyContent)
        {
            if (this.Status != Status.Wait)
            {
                return false;
            }
            this.Status = Status.Deny;
            this.ApprovedTime = DateTime.Now;
            this.ReplyContent = replyContent;
            return true;
        }

        public bool Lock()
        {
            if (this.Status != Status.Wait)
            {
                return false;
            }
            this.Status = Status.Lock;
            this.ApprovedTime = DateTime.Now;
            this.ReplyContent = "抱歉！您的JS权限申请没有被批准，并且申请已被锁定，具体请联系contact@cnblogs.com。";
            return true;
        }

        public async Task Passed()
        {
            if (this.Status != Status.Pass)
            {
                return;
            }
            eventBus = IocContainer.Default.Resolve<IEventBus>();
            await eventBus.Publish(new MessageSentEvent() { Title = "您的JS权限申请已批准", Content = this.ReplyContent, RecipientId = this.User.Id });
        }

        public async Task Denied()
        {
            if (this.Status != Status.Deny)
            {
                return;
            }
            eventBus = IocContainer.Default.Resolve<IEventBus>();
            await eventBus.Publish(new MessageSentEvent() { Title = "您的JS权限申请未通过审批", Content = this.ReplyContent, RecipientId = this.User.Id });
        }

        public async Task Locked()
        {
            if (this.Status != Status.Lock)
            {
                return;
            }
            eventBus = IocContainer.Default.Resolve<IEventBus>();
            await eventBus.Publish(new MessageSentEvent() { Title = "您的JS权限申请未通过审批", Content = this.ReplyContent, RecipientId = this.User.Id });
        }
    }
}
