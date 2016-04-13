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
using CNBlogs.Apply.Application.DTOs;

namespace CNBlogs.Apply.Application.Services
{
    public class JsPermissionApplyService : IJsPermissionApplyService
    {
        private IJsPermissionApplyRepository _jsPermissionApplyRepository;
        private IUnitOfWork _unitOfWork;

        public JsPermissionApplyService(IUnitOfWork unitOfWork,
            IJsPermissionApplyRepository jsPermissionApplyRepository)
        {
            _unitOfWork = unitOfWork;
            _jsPermissionApplyRepository = jsPermissionApplyRepository;
        }
    }
}
