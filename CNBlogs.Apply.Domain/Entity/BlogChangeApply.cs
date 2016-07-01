using CNBlogs.Apply.Domain.DomainEvents;
using CNBlogs.Apply.Domain.ValueObjects;
using CNBlogs.Apply.ServiceAgent;
using CNBlogs.Infrastructure.IoC.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace CNBlogs.Apply.Domain
{
    public class BlogChangeApply : ApplyAggregateRoot
    {
        public BlogChangeApply()
        { }

        public BlogChangeApply(string targetBlogApp, string reason, User user, string ip)
            : base(reason, user, ip)
        {
            if (string.IsNullOrEmpty(targetBlogApp))
            {
                throw new ArgumentException("博客地址不能为空");
            }
            this.TargetBlogApp = targetBlogApp;
        }

        public string TargetBlogApp { get; private set; }

        public Status GetStatus()
        {
            if (this.Status == Status.Deny && DateTime.Now > this.ApplyTime.AddDays(3))
            {
                return Status.None;
            }
            return this.Status;
        }

        public async Task<bool> Pass()
        {
            var replyContent = $"恭喜您！您的博客地址更改申请已通过，新的博客地址：<a href='{this.TargetBlogApp}' target='_blank'>{this.TargetBlogApp}</a>";
            return await base.Pass(replyContent, new BlogChangedEvent() { UserAlias = this.User.Alias, TargetUserAlias = this.TargetBlogApp });
        }

        public bool Lock()
        {
            var replyContent = "抱歉！您的博客地址更改申请没有被批准，并且申请已被锁定，具体请联系contact@cnblogs.com。";
            return base.Lock(replyContent);
        }

        public async Task Passed()
        {
            await base.Passed("您的博客地址更改申请已批准");
        }

        public async Task Denied()
        {
            await base.Passed("您的博客地址更改申请未通过审批");
        }

        public async Task Locked()
        {
            await Denied();
        }
    }
}
