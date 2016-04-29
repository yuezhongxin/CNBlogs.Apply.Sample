using CNBlogs.Infrastructure.IoC.Contracts;
using CNBlogs.Apply.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CNBlogs.Apply.Repository.Tests
{
    public class JsPermissionApplyRepositoryTest
    {
        private IJsPermissionApplyRepository _jsPermissionApplyRepository;

        public JsPermissionApplyRepositoryTest()
        {
            CNBlogs.Apply.BootStrapper.Startup.Configure();

            _jsPermissionApplyRepository = IocContainer.Default.Resolve<IJsPermissionApplyRepository>();
        }

        [Fact]
        public async Task GetAllTest()
        {
            var jsPermissionApplys = await _jsPermissionApplyRepository.GetAll().ToListAsync();
            Assert.NotNull(jsPermissionApplys);
        }

        [Fact]
        public async Task GetInvalidTest()
        {
            var userId = 435188;
            var jsPermissionApply = await _jsPermissionApplyRepository.GetInvalid(userId).FirstOrDefaultAsync();
            Assert.NotNull(jsPermissionApply);
        }

        [Fact]
        public async Task GetWaiting_ByUserIdTest()
        {
            var userId = 435188;
            var jsPermissionApply = await _jsPermissionApplyRepository.GetWaiting(userId).FirstOrDefaultAsync();
            Assert.NotNull(jsPermissionApply);
        }

        [Fact]
        public async Task GetWaitingTest()
        {
            var jsPermissionApplys = await _jsPermissionApplyRepository.GetWaiting().FirstOrDefaultAsync();
            Assert.NotNull(jsPermissionApplys);
        }
    }
}
