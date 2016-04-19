using CNBlogs.Apply.Domain.DomainServices;
using CNBlogs.Apply.Domain.ValueObjects;
using CNBlogs.Apply.Infrastructure.Interfaces;
using CNBlogs.Apply.Infrastructure.IoC.Contracts;
using CNBlogs.Apply.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Practices.Unity;

namespace CNBlogs.Apply.Domain.Tests
{
    public class JsPermissionApplyTest
    {
        private IApplyAuthenticationService _applyAuthenticationService;
        private IJsPermissionApplyRepository _jsPermissionApplyRepository;
        private IUnitOfWork _unitOfWork;

        public JsPermissionApplyTest()
        {
            CNBlogs.Apply.BootStrapper.Startup.Configure();

            _applyAuthenticationService = IocContainer.Default.Resolve<IApplyAuthenticationService>();
            _jsPermissionApplyRepository = IocContainer.Default.Resolve<IJsPermissionApplyRepository>();
            _unitOfWork = IocContainer.Default.Resolve<IUnitOfWork>();
        }

        [Fact]
        public async Task ApplyTest()
        {
            var userId = 1;
            var verfiyResult = await _applyAuthenticationService.Verfiy(userId);
            Console.WriteLine(verfiyResult);
            Assert.Empty(verfiyResult);

            var jsPermissionApply = new JsPermissionApply("我要申请JS权限", userId, "");
            _unitOfWork.RegisterNew(jsPermissionApply);
            Assert.True(await _unitOfWork.CommitAsync());
        }

        [Fact]
        public async Task ProcessApply_WithPassTest()
        {
            var userId = 1;
            var jsPermissionApply = await _jsPermissionApplyRepository.GetWaiting(userId).FirstOrDefaultAsync();
            Assert.NotNull(jsPermissionApply);

            await jsPermissionApply.Pass();
            _unitOfWork.RegisterDirty(jsPermissionApply);
            Assert.True(await _unitOfWork.CommitAsync());
        }

        [Fact]
        public async Task ProcessApply_WithDenyTest()
        {
            var userId = 1;
            var jsPermissionApply = await _jsPermissionApplyRepository.GetWaiting(userId).FirstOrDefaultAsync();
            Assert.NotNull(jsPermissionApply);

            await jsPermissionApply.Deny("理由太简单了。");
            _unitOfWork.RegisterDirty(jsPermissionApply);
            Assert.True(await _unitOfWork.CommitAsync());
        }

        [Fact]
        public async Task ProcessApply_WithLockTest()
        {
            var userId = 1;
            var jsPermissionApply = await _jsPermissionApplyRepository.GetWaiting(userId).FirstOrDefaultAsync();
            Assert.NotNull(jsPermissionApply);

            await jsPermissionApply.Lock();
            _unitOfWork.RegisterDirty(jsPermissionApply);
            Assert.True(await _unitOfWork.CommitAsync());
        }
    }
}
