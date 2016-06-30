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
    public class JsPermissionApplyService : IJsPermissionApplyService
    {
        private IApplyAuthenticationService _applyAuthenticationService;
        private IJsPermissionApplyRepository _jsPermissionApplyRepository;
        private IUnitOfWork _unitOfWork;

        public JsPermissionApplyService(IUnitOfWork unitOfWork,
            IApplyAuthenticationService applyAuthenticationService,
            IJsPermissionApplyRepository jsPermissionApplyRepository)
        {
            _unitOfWork = unitOfWork;
            _applyAuthenticationService = applyAuthenticationService;
            _jsPermissionApplyRepository = jsPermissionApplyRepository;
        }

        public async Task<Status> GetStatus(string userLoginName)
        {
            var user = await UserService.GetUserByLoginName(userLoginName);
            if (user == null || user?.Id == 0)
            {
                return Status.None;
            }
            var apply = await _jsPermissionApplyRepository.GetByUserId(user.Id).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (apply == null)
            {
                return Status.None;
            }
            return await apply.GetStatus(user.Alias);
        }

        public async Task<SubmitResult> Apply(string reason, string userLoginName, string ip)
        {
            var user = await UserService.GetUserByLoginName(userLoginName);
            var verfiyResult = await _applyAuthenticationService.VerfiyForJsPermission(user);
            if (!string.IsNullOrEmpty(verfiyResult))
            {
                return new SubmitResult { IsSucceed = false, Message = verfiyResult };
            }
            try
            {
                var jsPermissionApply = new JsPermissionApply(reason, user, ip);
                _unitOfWork.RegisterNew(jsPermissionApply);
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

        public async Task<List<JsPermissionApplyDTO>> GetWaitings()
        {
            return await _jsPermissionApplyRepository.GetWaiting()
                .OrderByDescending(x => x.ApplyTime)
                .ProjectTo<JsPermissionApplyDTO>().ToListAsync();
        }

        public async Task<int> GetWaitingCount()
        {
            return await _jsPermissionApplyRepository.GetWaiting().CountAsync();
        }

        public async Task<bool> Pass(int id)
        {
            var jsPermissionApply = await _jsPermissionApplyRepository.Get(id).FirstOrDefaultAsync();
            if (jsPermissionApply != null)
            {
                if (await jsPermissionApply.Pass())
                {
                    _unitOfWork.RegisterDirty(jsPermissionApply);
                    if (await _unitOfWork.CommitAsync())
                    {
                        await jsPermissionApply.Passed();
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<bool> Deny(int id, string replyContent)
        {
            var jsPermissionApply = await _jsPermissionApplyRepository.Get(id).FirstOrDefaultAsync();
            if (jsPermissionApply != null)
            {
                if (jsPermissionApply.Deny(replyContent))
                {
                    _unitOfWork.RegisterDirty(jsPermissionApply);
                    if (await _unitOfWork.CommitAsync())
                    {
                        await jsPermissionApply.Denied();
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<bool> Lock(int id)
        {
            var jsPermissionApply = await _jsPermissionApplyRepository.Get(id).FirstOrDefaultAsync();
            if (jsPermissionApply != null)
            {
                if (jsPermissionApply.Lock())
                {
                    _unitOfWork.RegisterDirty(jsPermissionApply);
                    if (await _unitOfWork.CommitAsync())
                    {
                        await jsPermissionApply.Locked();
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
