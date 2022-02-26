using AuthRoleDemo.Models;
using AuthRoleDemo.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthRoleDemo.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Login(AuthenticateRequest model);
        Task<IEnumerable<UserValid>> GetAll();
        Task<UserValid> GetById(int id);

        Task Add(UserValid user);
    }
}
