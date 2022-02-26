using AuthRoleDemo.Auth;
using AuthRoleDemo.Data;
using AuthRoleDemo.Helpers;
using AuthRoleDemo.Models;
using AuthRoleDemo.Models.Users;
using AuthRoleDemo.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace AuthRoleDemo.Services
{
    public class UserService : IUserService
    {
        private DataContext _dbContext;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public UserService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _dbContext = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }
        public async Task<IEnumerable<UserValid>> GetAll()
        {
            return _dbContext.Users;
        }

        public async Task<UserValid> GetById(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x=>x.Id==id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        public async Task<AuthenticateResponse> Login(AuthenticateRequest model)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == model.Username);
            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");
            //autentifikatsiya muvaffaqiyatli o'tdi, shuning uchun jwt tokenini yarating
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }

        public async Task Add(UserValid user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
