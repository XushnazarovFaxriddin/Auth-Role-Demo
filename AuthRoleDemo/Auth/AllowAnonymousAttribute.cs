using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthRoleDemo.Auth
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute:Attribute
    {
    }
}
