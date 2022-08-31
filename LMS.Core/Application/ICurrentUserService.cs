using System;

namespace LMS.Core.Application
{
    public interface ICurrentUserService
    {
        public Guid UserId { get; }
    }
}
