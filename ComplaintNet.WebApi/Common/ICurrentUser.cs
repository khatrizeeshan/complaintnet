using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace ComplaintNet.WebApi.Common
{
    public interface ICurrentUser
    {
        int Id { get; }
    }

    public class CurrentUser : ICurrentUser
    {
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            var value = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if(value != null) {
                this.Id = Convert.ToInt32(value);
            }
        }

        public int Id { get; }
    }
}
