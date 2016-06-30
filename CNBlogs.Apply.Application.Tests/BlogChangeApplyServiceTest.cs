using CNBlogs.Apply.Infrastructure.IoC.Contracts;
using CNBlogs.Apply.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Practices.Unity;

namespace CNBlogs.Apply.Application.Tests
{
    public class BlogChangeApplyServiceTest
    {
        private IBlogChangeApplyService _blogChangeApplyService;

        public BlogChangeApplyServiceTest()
        {
            CNBlogs.Apply.BootStrapper.Startup.Configure();

            _blogChangeApplyService = IocContainer.Default.Resolve<IBlogChangeApplyService>();
        }

        [Fact]
        public async Task ApplyTest()
        {
            var userLoginName = "田园里的蟋蟀";
            var targetBlogApp = "xishuai2";
            var result = await _blogChangeApplyService.Apply(targetBlogApp, "我想要修改博客地址", userLoginName, "");
            Console.WriteLine(result.Message);
            Assert.True(result.IsSucceed);
        }

        [Fact]
        public async Task Apply_WhenReasonEmptyTest()
        {
            var userLoginName = "田园里的蟋蟀";
            var targetBlogApp = "xishuai2";
            var result = await _blogChangeApplyService.Apply(targetBlogApp, "", userLoginName, "");
            Console.WriteLine(result.Message);
            Assert.True(result.IsSucceed);
        }

        [Fact]
        public async Task GetWatingsTest()
        {
            var applys = await _blogChangeApplyService.GetWaitings();
            Assert.NotNull(applys);
            applys.ForEach(x=> Console.WriteLine($"Id: {x.Id}, UserId: {x.UserId}, Ip: {x.Ip}, ApplyTime: {x.ApplyTime}, TargetBlogApp: {x.TargetBlogApp}, Reason: {x.Reason}"));
        }

        [Fact]
        public async Task PassTest()
        {
            var id = 5;
            Assert.True(await _blogChangeApplyService.Pass(id));
        }

        [Fact]
        public async Task DenyTest()
        {
            var id = 8;
            Assert.True(await _blogChangeApplyService.Deny(id, "理由太简单了。"));
        }

        [Fact]
        public async Task LockTest()
        {
            var id = 8;
            Assert.True(await _blogChangeApplyService.Pass(id));
        }


        [Fact]
        public async Task GetStatusTest()
        {
            var userLoginName = "田园里的蟋蟀";
            var result = await _blogChangeApplyService.GetStatus(userLoginName);
            Assert.Equal(result, Domain.ValueObjects.Status.Pass);
        }
    }
}
