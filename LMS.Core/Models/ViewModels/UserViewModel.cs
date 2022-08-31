using System;
using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Eid { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsMale { get; set; }
        public bool IsActiveInLMS { get; set; }
        public List<SystemModule> SystemModules { get; set; }
        public DateTimeOffset? DateOfJoin { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
        public string Avatar { get; set; }
    }
}
