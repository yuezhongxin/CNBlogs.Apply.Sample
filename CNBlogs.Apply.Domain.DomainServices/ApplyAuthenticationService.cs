using CNBlogs.Apply.Domain.ValueObjects;
using CNBlogs.Apply.Repository.Interfaces;
using CNBlogs.Apply.ServiceAgent;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Domain.DomainServices
{
    public class ApplyAuthenticationService : IApplyAuthenticationService
    {
        private IJsPermissionApplyRepository _jsPermissionApplyRepository;

        public ApplyAuthenticationService(IJsPermissionApplyRepository jsPermissionApplyRepository)
        {
            _jsPermissionApplyRepository = jsPermissionApplyRepository;
        }

        public async Task<string> Verfiy(User user)
        {
            if (user == null)
            {
                return "未获取用户！";
            }
            if (string.IsNullOrEmpty(user.Alias) && user.Alias != user.Id.ToString())
            {
                return "必须先开通博客，才能申请JS权限！";
            }
            var entity = await _jsPermissionApplyRepository.GetInvalid(user.Id).FirstOrDefaultAsync();
            if (entity != null)
            {
                if (entity.Status == Status.Pass)
                {
                    return "您的JS权限申请已开通，请勿重复申请！";
                }
                if (entity.Status == Status.Wait)
                {
                    return "您的JS权限申请正在处理中，请稍！";
                }
                if (entity.Status == Status.Lock)
                {
                    return "您暂时无法申请JS权限，请联系contact@cnblogs.com";
                }
            }
            return string.Empty;
        }
    }
}
