using AutoMapper;
using BackendBooking.Authorization;
using BackendBooking.Helpers;
using BackendBooking.Interface;
using BackendBooking.Models.User;
using BackendBooking.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BackendBooking.Controllers.User
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        #region Authenticate
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            return Ok(response);
        }
        #endregion

        #region Register
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            _userService.Register(model);
            return Ok(new { message = "Registration successful" });
        }
        #endregion

        #region GetAllUsers
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
        #endregion

        #region GetUserById
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            return Ok(user);
        }
        #endregion

        #region GetUserViewById
        [HttpGet("{id}/View")] // Get by Id 
        public IActionResult GetView(int id)
        {
            var user = _userService.GetViewById(id);
            return Ok(user);
        }
        #endregion

        #region UpdateUser
        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateUserRequest model)
        {
            _userService.Update(id, model);
            return Ok(new { message = "User updated successfully" });
        }
        #endregion

        #region DeleteUser
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok(new { message = "User deleted successfully" });
        }
        #endregion

        #region GetCurrentUser
        [HttpGet("Me")]
        public IActionResult GetCurrentUser()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userId = _jwtUtils.ValidateToken(token);
            if (userId == null)
                return Unauthorized(new { message = "Unauthorized" });

            var userMe = _userService.GetCurrentUserDto(userId.Value);
            return Ok(userMe);
        }
        #endregion
    }
}
