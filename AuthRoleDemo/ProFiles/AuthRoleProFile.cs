using AuthRoleDemo.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthRoleDemo.ProFiles
{
    public class AuthRoleProFile:Profile
    {
        public AuthRoleProFile()
        {
            CreateMap<UserValid, User>();
            CreateMap<User, UserValid>();
        }
    }
}
