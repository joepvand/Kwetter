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
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("USER ID CLAIM!!!! :" + context.Request.Headers["claims_userid"].ToString() + "EBDDD!!!!!");
            var id = context.Request.Headers["claims_userid"].ToString();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

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
