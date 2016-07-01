using CNBlogs.Apply.Domain.ValueObjects;
using CNBlogs.Apply.Repository.Interfaces;
using CNBlogs.Apply.ServiceAgent;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Domain.DomainServices
{
    public class ApplyAuthenticationService : IApplyAuthenticationService
    {
        private IJsPermissionApplyRepository _jsPermissionApplyRepository;
        private IBlogChangeApplyRepository _blogChangeApplyRepository;

        public ApplyAuthenticationService(IJsPermissionApplyRepository jsPermissionApplyRepository,
            IBlogChangeApplyRepository blogChangeApplyRepository)
        {
            _jsPermissionApplyRepository = jsPermissionApplyRepository;
            _blogChangeApplyRepository = blogChangeApplyRepository;
        }

        public async Task<string> VerfiyForJsPermission(User user)
        {
            if (user == null || user?.Id == 0)
            {
                return "未获取用户！";
            }
            if (string.IsNullOrEmpty(user.Alias) && user.Alias != user.Id.ToString())
            {
                return "必须先开通博客，才能申请JS权限！";
            }
            var apply = await _jsPermissionApplyRepository.GetByUserId(user.Id).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (apply != null)
            {
                var applyStatus = await apply.GetStatus(user.Alias);
                switch (applyStatus)
                {
                    case Status.Wait:
                        return "您的JS权限申请正在处理中，请耐心等待！";
                    case Status.Pass:
                        return "您的JS权限已开通，请勿重复申请！";
                    case Status.Lock:
                        return "您暂时无法申请JS权限，请联系contact@cnblogs.com";
                    default:
                        break;
                }
            }
            return string.Empty;
        }

        public async Task<string> VerfiyForBlogChange(User user, string targetBlogApp)
        {
            if (string.IsNullOrEmpty(targetBlogApp))
            {
                return "博客地址不能为空";
            }
            if (user == null || user?.Id == 0)
            {
                return "未获取用户！";
            }
            if (string.IsNullOrEmpty(user.Alias) && user.Alias != user.Id.ToString())
            {
                return "必须先开通博客，才能更改博客地址！";
            }
            targetBlogApp = targetBlogApp.Trim();
            if (user.Alias.Equals(targetBlogApp))
            {
                return "修改博客地址不能和原地址相同！";
            }
            if (targetBlogApp.Length < 4)
            {
                return "博客地址至少4个字符！";
            }
            if (!Regex.IsMatch(targetBlogApp, @"^([0-9a-zA-Z_-])+$"))
            {
                return "博客地址只能使用英文、数字、-连字符、_下划线！";
            }
            var apply = await _blogChangeApplyRepository.GetByUserId(user.Id).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (apply != null)
            {
                var applyStatus = apply.GetStatus();
                switch (applyStatus)
                {
                    case Status.Wait:
                        return "您的博客地址更改申请正在处理中，请耐心等待！";
                    case Status.Lock:
                        return "您暂时无法更改博客地址，请联系contact@cnblogs.com";
                    default:
                        break;
                }
            }
            if (await BlogService.ExistBlogApp(targetBlogApp) || await _blogChangeApplyRepository.GetByTargetAliasWithWait(targetBlogApp).AnyAsync())
            {
                return "此博客地址已被使用，请更换！";
            }
            return string.Empty;
        }
    }
}
