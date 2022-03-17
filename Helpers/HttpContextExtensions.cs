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
        public static string GetUserId(this HttpContext context)
        {
            var id = context.Request.Headers["claims_userid"].ToString();
            return id;
        }
    }
}
