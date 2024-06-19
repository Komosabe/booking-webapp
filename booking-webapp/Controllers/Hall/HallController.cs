using BackendBooking.Authorization;
using BackendBooking.Interface;
using BackendBooking.Models.Hall;
using Microsoft.AspNetCore.Mvc;

namespace BackendBooking.Controllers.Hall
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HallController : Controller
    {
        private readonly IHallService _hallService;

        public HallController(IHallService hallService)
        {
            _hallService = hallService;
        }

        #region CreateHall
        [HttpPost("CreateHall")]
        public async Task<IActionResult> AddHall(CreateHallModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var hallId = await _hallService.CreateHallAsync(model, token);
                return Ok($"Sala została dodana prawidłowo. Identyfikator sali: {hallId}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd podczas dodawania sali: {ex.Message}");
            }
        }
        #endregion

        #region DeleteHall
        [HttpDelete("DeleteHall/{hallId}")]
        public async Task<IActionResult> DeleteHall(int hallId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _hallService.DeleteHallAsync(hallId);
                return Ok("Sala została usunięta prawidłowo");
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd podczas usuwania sali: {ex.Message}");
            }
        }
        #endregion

        #region GetAllHalls
        [HttpGet("GetAllHalls")]
        public async Task<IActionResult> GetAllHalls()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var halls = await _hallService.GetAllHallsAsync(token);
                return Ok(halls);
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd podczas pobierania wszystkich sal: {ex.Message}");
            }
        }
        #endregion

        #region GetHallByid
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHall([FromRoute] int id)
        {
            try
            {
                var hall = await _hallService.GetHallByIdAsync(id);
                return Ok(hall);
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd podczas pobierania sali: {ex.Message}");
            }
        }
        #endregion

        #region UpdateHall
        [HttpPut("UpdateHall/{hallId}")]
        public async Task<IActionResult> UpdateHall([FromRoute] int hallId, [FromBody] UpdateHallModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _hallService.UpdateHallAsync(hallId, model);
                return Ok("Dane sali zostały zaktualizowane prawidłowo");
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd podczas aktualizacji danych sali: {ex.Message}");
            }
        }
        #endregion
    }
}
