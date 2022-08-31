using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Models.ViewModels
{
    public class UserModel
    {
        [JsonProperty("access_token")]
        public string TMSAccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [Key]
        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [NotMapped]
        [JsonProperty("role")]
        public List<string> Roles { get; set; }

        [NotMapped]
        [JsonProperty("systemModules")]
        public List<SystemModule> SystemModules { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
