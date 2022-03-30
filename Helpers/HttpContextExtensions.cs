using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class HttpContextExtensions
    {
        public static Guid GetUserId(this HttpContext context)
        {
            var id = context.Request.Headers["claims_userid"].ToString();
            return Guid.Parse(id);
        }
        public static Role GetUserRole(this HttpContext context)
        {
            return Enum.Parse<Role>(context.Request.Headers["claims_role"].ToString());
        }
    }

    public enum Role
    {
        User,
        Admin
    }
}
