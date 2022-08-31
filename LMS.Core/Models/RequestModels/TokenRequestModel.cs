using System.ComponentModel.DataAnnotations;

namespace LMS.Core.Models.RequestModels
{
    public class TokenRequestModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
