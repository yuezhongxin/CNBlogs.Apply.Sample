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
    public class JsPermissionApplyServiceTest
    {
        private IJsPermissionApplyService _jsPermissionApplyService;

        public JsPermissionApplyServiceTest()
        {
            CNBlogs.Apply.BootStrapper.Startup.Configure();

            _jsPermissionApplyService = IocContainer.Default.Resolve<IJsPermissionApplyService>();
        }

        [Fact]
        public async Task ApplyTest()
        {
            var userLoginName = "田园里的蟋蟀";
            var result = await _jsPermissionApplyService.Apply("我想要申请JS权限", userLoginName, "");
            Console.WriteLine(result.Message);
            Assert.True(result.IsSucceed);
        }

        [Fact]
        public async Task Apply_WhenReasonEmptyTest()
        {
            var userLoginName = "田园里的蟋蟀";
            var result = await _jsPermissionApplyService.Apply("", userLoginName, "");
            Console.WriteLine(result.Message);
            Assert.True(result.IsSucceed);
        }

        [Fact]
        public async Task GetWatingsTest()
        {
            var applys = await _jsPermissionApplyService.GetWaitings();
            Assert.NotNull(applys);
            applys.ForEach(x=> Console.WriteLine($"Id: {x.Id}, UserId: {x.UserId}, Ip: {x.Ip}, ApplyTime: {x.ApplyTime}, Reason: {x.Reason}"));
        }

        [Fact]
        public async Task PassTest()
        {
            var id = 8;
            Assert.True(await _jsPermissionApplyService.Pass(id));
        }

        [Fact]
        public async Task DenyTest()
        {
            var id = 8;
            Assert.True(await _jsPermissionApplyService.Deny(id, "理由太简单了。"));
        }

        [Fact]
        public async Task LockTest()
        {
            var id = 8;
            Assert.True(await _jsPermissionApplyService.Pass(id));
        }


        [Fact]
        public async Task GetStatusTest()
        {
            var userLoginName = "田园里的蟋蟀";
            var result = await _jsPermissionApplyService.GetStatus(userLoginName);
            Assert.Equal(result, Domain.ValueObjects.Status.None);
        }
    }
}
