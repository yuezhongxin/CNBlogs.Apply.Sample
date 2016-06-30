using CNBlogs.Apply.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNBlogs.Apply.Repository.Interfaces;
using CNBlogs.Apply.Infrastructure.Interfaces;
using CNBlogs.Apply.Infrastructure;
using System.Data.Entity;
using EntityFramework.Extensions;
using CNBlogs.Apply.Domain.DomainServices;
using CNBlogs.Apply.Domain;
using CNBlogs.Apply.Application.DTOs;
using AutoMapper.QueryableExtensions;
using CNBlogs.Apply.ServiceAgent;
using CNBlogs.Apply.Domain.ValueObjects;

namespace CNBlogs.Apply.Application.Services
{
    public class BlogChangeApplyService : IBlogChangeApplyService
    {
        private IApplyAuthenticationService _applyAuthenticationService;
        private IBlogChangeApplyRepository _blogChangeApplyRepository;
        private IUnitOfWork _unitOfWork;

        public BlogChangeApplyService(IUnitOfWork unitOfWork,
            IApplyAuthenticationService applyAuthenticationService,
            IBlogChangeApplyRepository blogChangeApplyRepository)
        {
            _unitOfWork = unitOfWork;
            _applyAuthenticationService = applyAuthenticationService;
            _blogChangeApplyRepository = blogChangeApplyRepository;
        }

        public async Task<Status> GetStatus(string userLoginName)
        {
            var user = await UserService.GetUserByLoginName(userLoginName);
            if (user == null || user?.Id == 0)
            {
                return Status.None;
            }
            var apply = await _blogChangeApplyRepository.GetByUserId(user.Id).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (apply == null)
            {
                return Status.None;
            }
            return apply.GetStatus();
        }

        public async Task<SubmitResult> Apply(string targetBlogApp, string reason, string userLoginName, string ip)
        {
            var user = await UserService.GetUserByLoginName(userLoginName);
            var verfiyResult = await _applyAuthenticationService.VerfiyForBlogChange(user, targetBlogApp);
            if (!string.IsNullOrEmpty(verfiyResult))
            {
                return new SubmitResult { IsSucceed = false, Message = verfiyResult };
            }
            try
            {
                var blogChangeApply = new BlogChangeApply(targetBlogApp ,reason, user, ip);
                _unitOfWork.RegisterNew(blogChangeApply);
                return new SubmitResult { IsSucceed = await _unitOfWork.CommitAsync() };
            }
            catch (ArgumentException ae)
            {
                return new SubmitResult { IsSucceed = false, Message = ae.Message };
            }
            catch (Exception ex)
            {
                return new SubmitResult { IsSucceed = false, Message = ex.Message };
            }
        }

        public async Task<List<BlogChangeApplyDTO>> GetWaitings()
        {
            return await _blogChangeApplyRepository.GetWaiting()
                .OrderByDescending(x => x.ApplyTime)
                .ProjectTo<BlogChangeApplyDTO>().ToListAsync();
        }

        public async Task<int> GetWaitingCount()
        {
            return await _blogChangeApplyRepository.GetWaiting().CountAsync();
        }

        public async Task<bool> Pass(int id)
        {
            var blogChangeApply = await _blogChangeApplyRepository.Get(id).FirstOrDefaultAsync();
            if (blogChangeApply != null)
            {
                if (await blogChangeApply.Pass())
                {
                    _unitOfWork.RegisterDirty(blogChangeApply);
                    if (await _unitOfWork.CommitAsync())
                    {
                        await blogChangeApply.Passed();
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<bool> Deny(int id, string replyContent)
        {
            var blogChangeApply = await _blogChangeApplyRepository.Get(id).FirstOrDefaultAsync();
            if (blogChangeApply != null)
            {
                if (blogChangeApply.Deny(replyContent))
                {
                    _unitOfWork.RegisterDirty(blogChangeApply);
                    if (await _unitOfWork.CommitAsync())
                    {
                        await blogChangeApply.Denied();
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<bool> Lock(int id)
        {
            var blogChangeApply = await _blogChangeApplyRepository.Get(id).FirstOrDefaultAsync();
            if (blogChangeApply != null)
            {
                if (blogChangeApply.Lock())
                {
                    _unitOfWork.RegisterDirty(blogChangeApply);
                    if (await _unitOfWork.CommitAsync())
                    {
                        await blogChangeApply.Locked();
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
