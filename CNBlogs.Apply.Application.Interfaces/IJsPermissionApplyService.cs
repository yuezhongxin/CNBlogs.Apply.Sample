using CNBlogs.Apply.Application.DTOs;
using CNBlogs.Apply.Domain.ValueObjects;
using CNBlogs.Apply.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Application.Interfaces
{
    public interface IJsPermissionApplyService
    {
        Task<Status> GetStatus(string userLoginName);

        Task<SubmitResult> Apply(string reason, string userLoginName, string ip);

        Task<List<JsPermissionApplyDTO>> GetWaitings();

        Task<int> GetWaitingCount();

        Task<bool> Pass(int id);

        Task<bool> Deny(int id, string replyContent);

        Task<bool> Lock(int id);
    }
}
