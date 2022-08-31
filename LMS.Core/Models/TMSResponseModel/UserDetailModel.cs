using LMS.Core.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LMS.Core.Models.TMSResponseModel
{
    public class UserDetailModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        //staff id 
        [JsonProperty("eid")]
        public string Eid { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("gender")]
        public bool IsMale { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("dateOfBirth")]
        public string DateOfBirthString { get; set; }

        [JsonProperty("systemModules")]
        public List<SystemModule> SystemModules { get; set; }
    }
}
