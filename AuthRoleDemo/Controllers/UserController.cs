using AuthRoleDemo.Auth;
using AuthRoleDemo.DTOs;
using AuthRoleDemo.Entites;
using AuthRoleDemo.Models;
using AuthRoleDemo.Models.Users;
using AuthRoleDemo.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthRoleDemo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(AuthenticateRequest model)
        {
            var response = await _userService.Login(model);
            return Ok(response);
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            // only admins can access other user records
            var currentUser = (User)HttpContext.Items["User"];
            if (id != currentUser.Id && currentUser.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            var user = await _userService.GetById(id);
            return Ok(user);
        }

        [Authorize(Role.Admin)]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] AddUserDTO userDto)
        {
            if (userDto is null)
                return BadRequest(new { message = "Barcha maydonlarni to'ldirish majburiy." });
            var user = new UserValid();
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Username = userDto.Username;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            user.Role = Role.User;
            await _userService.Add(user);
            return Ok(true);
        }
    }
}
