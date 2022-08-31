using System;
using System.Threading.Tasks;
using AutoMapper;
using LMS.Core.Enum;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.ViewModels;

namespace LMS.Infrastructure.Services
{
    public class PermissionCategoryService : IPermissionCategoryService
    {
        private IMapper _mapper;

        public PermissionCategoryService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<PermissionCategoryViewModel> GetAllPermissionCategories()
        {
            return Task.FromResult(_mapper.Map<PermissionCategoryViewModel>(Enum.GetValues(typeof(PermissionCategory))));
        }
    }
}