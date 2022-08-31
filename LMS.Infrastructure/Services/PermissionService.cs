using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LMS.Core.Entity;
using LMS.Core.Enum;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Utils;

namespace LMS.Infrastructure.Services
{
    public class PermissionService : IPermissionService
    {
        private IPermissionRepository _permissionRepository;
        private IMapper _mapper;

        public PermissionService(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }
        public Task<IQueryable<PermissionViewModel>> GetPermissionByCategory(string category)
        {
            ValidateUtils.CheckStringNotEmpty("category", category);
            PermissionCategory parseCategory;
            IQueryable<Permission> permissions = null;
            if (Enum.TryParse(category, out parseCategory))
            {
                permissions = _permissionRepository.Get(p => p.Category == parseCategory).OrderBy(r => r.Id);
            }
            if (permissions == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            return Task.FromResult(permissions.ProjectTo<PermissionViewModel>(_mapper.ConfigurationProvider));
        }
    }
}