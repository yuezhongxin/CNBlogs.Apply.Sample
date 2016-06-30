using CNBlogs.Apply.Domain.DomainEvents;
using CNBlogs.Apply.Domain.ValueObjects;
using CNBlogs.Apply.ServiceAgent;
using CNBlogs.Apply.Infrastructure.IoC.Contracts;
using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Practices.Unity;

namespace CNBlogs.Apply.Domain
{
    public class ApplyAggregateRoot : IAggregateRoot
    {
        private IEventBus eventBus;

        public ApplyAggregateRoot()
        { }

        public ApplyAggregateRoot(string reason, User user, string ip)
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

        public int Id { get; protected set; }

        public string Reason { get; protected set; }

        public virtual User User { get; protected set; }

        public Status Status { get; protected set; } = Status.Wait;

        public string Ip { get; protected set; }

        public DateTime ApplyTime { get; protected set; } = DateTime.Now;

        public string ReplyContent { get; protected set; }

        public DateTime? ApprovedTime { get; protected set; }

        public bool IsActive { get; protected set; } = true;

        protected async Task<bool> Pass<TEvent>(string replyContent, TEvent @event)
            where TEvent : IEvent
        {
            if (this.Status != Status.Wait)
            {
                return false;
            }
            this.Status = Status.Pass;
            this.ApprovedTime = DateTime.Now;
            this.ReplyContent = replyContent;
            eventBus = IocContainer.Default.Resolve<IEventBus>();
            await eventBus.Publish(@event);
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

        protected bool Lock(string replyContent)
        {
            if (this.Status != Status.Wait)
            {
                return false;
            }
            this.Status = Status.Lock;
            this.ApprovedTime = DateTime.Now;
            this.ReplyContent = replyContent;
            return true;
        }

        protected async Task Passed(string title)
        {
            if (this.Status != Status.Pass)
            {
                return;
            }
            await SendMessage(title);
        }

        protected async Task Denied(string title)
        {
            if (this.Status != Status.Deny)
            {
                return;
            }
            await SendMessage(title);
        }

        protected async Task Locked(string title)
        {
            if (this.Status != Status.Lock)
            {
                return;
            }
            await SendMessage(title);
        }

        private async Task SendMessage(string title)
        {
            eventBus = IocContainer.Default.Resolve<IEventBus>();
            await eventBus.Publish(new MessageSentEvent() { Title = title, Content = this.ReplyContent, RecipientId = this.User.Id });
        }
    }
}
