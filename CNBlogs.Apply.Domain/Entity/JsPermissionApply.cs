using CNBlogs.Apply.Domain.DomainEvents;
using CNBlogs.Apply.Domain.ValueObjects;
using CNBlogs.Apply.ServiceAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using CNBlogs.Apply.Infrastructure.IoC.Contracts;

namespace CNBlogs.Apply.Domain
{
    public class JsPermissionApply : IAggregateRoot
    {
        private IEventBus eventBus;

        public JsPermissionApply()
        { }

        public JsPermissionApply(string reason, int userId, string ip)
        {
            if (string.IsNullOrEmpty(reason))
            {
                throw new ArgumentException("申请内容不能为空");
            }
            if (reason.Length > 3000)
            {
                throw new ArgumentException("申请内容超出最大长度");
            }
            if (userId == 0)
            {
                throw new ArgumentException("用户Id为0");
            }
            this.Reason = reason;
            this.UserId = userId;
            this.Ip = ip;
            this.Status = Status.Wait;
        }

        public int Id { get; private set; }

        public string Reason { get; private set; }

        public int UserId { get; private set; }

        public Status Status { get; private set; } = Status.Wait;

        public string Ip { get; private set; }

        public DateTime ApplyTime { get; private set; } = DateTime.Now;

        public string ReplyContent { get; private set; }

        public DateTime? ApprovedTime { get; private set; }

        public bool IsActive { get; private set; } = true;

        public void Process(string replyContent, Status status)
        {
            this.ReplyContent = replyContent;
            this.Status = status;
            this.ApprovedTime = DateTime.Now;

            eventBus = IocContainer.Default.Resolve<IEventBus>();
            if (this.Status == Status.Pass)
            {
                eventBus.Publish(new JsPermissionOpenedEvent() { UserId = this.UserId });
                eventBus.Publish(new MessageSentEvent() { Title = "系统通知", Content = "审核通过", RecipientId = this.UserId });
            }
            else if (this.Status == Status.Deny)
            {
                eventBus.Publish(new MessageSentEvent() { Title = "系统通知", Content = "审核不通过", RecipientId = this.UserId });
            }
        }
    }
}
