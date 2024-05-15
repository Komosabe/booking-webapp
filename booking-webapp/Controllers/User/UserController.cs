using BackendBooking.Interface;
using BackendBooking.Models.User;
using BackendBooking.Service;
using Microsoft.AspNetCore.Mvc;

namespace BackendBooking.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IReservationService _reservationService;

        public UserController(IUserService userService, IReservationService reservationService)
        {
            _userService = userService;
            _reservationService = reservationService;
        }

        #region CreateUser
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var userid = await _userService.CreateUserAsync(model);
                return Ok($"User dodany prawidłowo. Identyfikator user: {userid}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd podczas tworzenia usera: {ex.Message}");
            }
            
        }
        #endregion

        #region DeleteUser
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userService.DeleteUserAsync(id);

            return Ok("User usunięty prawidłowo.");
        }
        #endregion

        #region GetAllUsers
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUserAsync();
            return Ok(users);
        }
        #endregion

        #region GetUserById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd podczas pobierania użytkownika: {ex.Message}");
            }
        }
        #endregion

        #region UpdateUser
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _userService.UpdateUserAsync(model);
                return Ok("Dane użytkownika zostały zaktualizowane prawidłowo");
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd podczas aktualizacji danych użytkownika: {ex.Message}");
            }
        }
        #endregion

        #region GetReservationAllForUser
        [HttpGet("GetReservationsForUser/{userId}")]
        public async Task<IActionResult> GetReservationsForUser(int userId)
        {
            try
            {
                var reservations = await _reservationService.GetReservationsForUserAsync(userId);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd podczas pobierania rezerwacji dla użytkownika: {ex.Message}");
            }
        }
        #endregion

        #region Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                var isAuthenticated = await _userService.AuthenticateAsync(model.Username, model.Password);
                if (isAuthenticated)
                {
                    return Ok("Login successful");
                }
                else
                {
                    return BadRequest("Invalid username or password");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error during user authentication: {ex.Message}");
            }
        }
        #endregion
    }
}
