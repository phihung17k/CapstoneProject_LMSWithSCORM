using LMS.API.Permission;
using LMS.Core.Common;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LMS.API.Hubs
{
    [PermissionAuthorize(PermissionConstants.BasePermission.ViewProfile)]
    public class NotificationHub : Hub
    {
        public static ConcurrentDictionary<string, string> MyUsers = new ConcurrentDictionary<string, string>();

        public override Task OnConnectedAsync()
        {
            //string userId = Context.UserIdentifier;
            string userId = Context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            MyUsers.TryAdd(Context.ConnectionId, userId);
            return base.OnConnectedAsync();
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }

        public string GetUserIdentifier()
        {
            return Context.UserIdentifier;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            MyUsers.TryRemove(Context.ConnectionId, out string userId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
