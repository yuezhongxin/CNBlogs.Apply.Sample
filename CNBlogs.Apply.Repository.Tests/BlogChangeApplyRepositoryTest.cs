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

namespace CNBlogs.Apply.Repository.Tests
{
    public class BlogChangeApplyRepositoryTest
    {
        private IBlogChangeApplyRepository _blogChangeApplyRepository;

        public BlogChangeApplyRepositoryTest()
        {
            CNBlogs.Apply.BootStrapper.Startup.Configure();

            _blogChangeApplyRepository = IocContainer.Default.Resolve<IBlogChangeApplyRepository>();
        }

        [Fact]
        public async Task GetByTargetAliasWithWaitTest()
        {
            var targetBlogApp = "xishuai";
            var result = await _blogChangeApplyRepository.GetByTargetAliasWithWait(targetBlogApp).AnyAsync();
            Assert.False(result);
        }
    }
}
