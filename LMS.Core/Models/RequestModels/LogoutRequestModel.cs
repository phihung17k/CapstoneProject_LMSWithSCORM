using System.ComponentModel.DataAnnotations;

namespace LMS.Core.Models.RequestModels
{
    public class LogoutRequestModel
    {
        [Required]
        public string UserId { get; set; }
    }
}
