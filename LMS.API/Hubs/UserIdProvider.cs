using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace LMS.API.Hubs
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            //return connection.UserIdentifier;
        }
    }
}
