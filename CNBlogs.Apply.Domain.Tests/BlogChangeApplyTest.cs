using CNBlogs.Apply.Domain.DomainEvents;
using CNBlogs.Apply.Domain.DomainServices;
using CNBlogs.Apply.Domain.ValueObjects;
using CNBlogs.Apply.Infrastructure.Interfaces;
using CNBlogs.Apply.Repository.Interfaces;
using CNBlogs.Apply.Infrastructure.IoC.Contracts;
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
    public class BlogChangeApplyTest
    {
        private IApplyAuthenticationService _applyAuthenticationService;
        private IBlogChangeApplyRepository _blogChangeApplyRepository;
        private IUnitOfWork _unitOfWork;

        public BlogChangeApplyTest()
        {
            CNBlogs.Apply.BootStrapper.Startup.Configure();

            _applyAuthenticationService = IocContainer.Default.Resolve<IApplyAuthenticationService>();
            _blogChangeApplyRepository = IocContainer.Default.Resolve<IBlogChangeApplyRepository>();
            _unitOfWork = IocContainer.Default.Resolve<IUnitOfWork>();
        }

        [Fact]
        public async Task ApplyTest()
        {
            var targetBlogApp = "xishuai2";
            var user = new User
            {
                Alias = "xishuai",
                DisplayName = "田园里的蟋蟀",
                Id = 435188
            };
            var verfiyResult = await _applyAuthenticationService.VerfiyForBlogChange(user, targetBlogApp);
            Console.WriteLine(verfiyResult);
            Assert.Empty(verfiyResult);

            try
            {
                var blogChangeApply = new BlogChangeApply(targetBlogApp, "我要申请修改博客地址", user, "127.0.0.1");
                _unitOfWork.RegisterNew(blogChangeApply);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
                Assert.True(false);
            }
            Assert.True(await _unitOfWork.CommitAsync());
        }

        [Fact]
        public async Task ApplyWithScriptTest()
        {
            var targetBlogApp = "xishuai2";
            var user = new User
            {
                Alias = "xishuai",
                DisplayName = "田园里的蟋蟀",
                Id = 435188
            };
            var verfiyResult = await _applyAuthenticationService.VerfiyForBlogChange(user, targetBlogApp);
            Console.WriteLine(verfiyResult);
            Assert.Empty(verfiyResult);

            try
            {
                var blogChangeApply = new BlogChangeApply(targetBlogApp, "我要申请修改博客地址<script>console.log(111)</script>", user, "127.0.0.1");
                _unitOfWork.RegisterNew(blogChangeApply);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
                Assert.True(false);
            }
            Assert.True(await _unitOfWork.CommitAsync());
        }

        [Fact]
        public async Task ProcessApply_WithPassTest()
        {
            var userId = 435188;
            var blogChangeApply = await _blogChangeApplyRepository.GetWaiting(userId).FirstOrDefaultAsync();
            Assert.NotNull(blogChangeApply);

            Assert.True(await blogChangeApply.Pass());
            _unitOfWork.RegisterDirty(blogChangeApply);
            Assert.True(await _unitOfWork.CommitAsync());
            await blogChangeApply.Passed();
        }

        [Fact]
        public async Task ProcessApply_WithDenyTest()
        {
            var userId = 435188;
            var blogChangeApply = await _blogChangeApplyRepository.GetWaiting(userId).FirstOrDefaultAsync();
            Assert.NotNull(blogChangeApply);

            Assert.True(blogChangeApply.Deny("理由太简单了。"));
            _unitOfWork.RegisterDirty(blogChangeApply);
            Assert.True(await _unitOfWork.CommitAsync());
            await blogChangeApply.Denied();
        }

        [Fact]
        public async Task ProcessApply_WithLockTest()
        {
            var userId = 435188;
            var blogChangeApply = await _blogChangeApplyRepository.GetWaiting(userId).FirstOrDefaultAsync();
            Assert.NotNull(blogChangeApply);

            Assert.True(blogChangeApply.Lock());
            _unitOfWork.RegisterDirty(blogChangeApply);
            Assert.True(await _unitOfWork.CommitAsync());
            await blogChangeApply.Locked();
        }
    }
}
