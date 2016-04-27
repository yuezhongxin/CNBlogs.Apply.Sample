using CNBlogs.Apply.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Practices.Unity;
using CNBlogs.Apply.Infrastructure.IoC.Contracts;

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
    }
}
