using BackendBooking.Authorization;
using BackendBooking.Interface;
using BackendBooking.Models.Concert;
using Microsoft.AspNetCore.Mvc;

namespace BackendBooking.Controllers.Concert
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ConcertController : Controller
    {
        private readonly IConcertService _concertService;

        public ConcertController(IConcertService concertService)
        {
            _concertService = concertService;
        }

        #region CreateConcert
        [HttpPost("CreateConcert")]
        public async Task<IActionResult> AddConcert(CreateConcertModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var concertId = await _concertService.CreateConcertAsync(model);
                return Ok($"Koncert został dodany prawidłowo. Identyfikator koncertu: {concertId}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd podczas dodawania koncertu: {ex.Message}");
            }
        }
        #endregion

        #region DeleteConcert
        [HttpDelete("DeleteConcert")]
        public async Task<IActionResult> DeleteConcert(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _concertService.DeleteConcertAsync(id);
                return Ok("Koncert został usunięty prawidłowo");
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd podczas usuwania koncertu: {ex.Message}");
            }
        }
        #endregion

        #region GetAllConcerts
        [HttpGet("GetAllConcerts")]
        public async Task<IActionResult> GetAllConcerts()
        {
            try
            {
                var concerts = await _concertService.GetAllConcertsAsync();
                return Ok(concerts);
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd podczas pobierania wszystkich koncertów: {ex.Message}");
            }
        }
        #endregion

        #region GetConcertByid
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConcert(int id)
        {
            try
            {
                var concert = await _concertService.GetConcertByIdAsync(id);
                return Ok(concert);
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd podczas pobierania koncertu: {ex.Message}");
            }
        }
        #endregion

        #region UpdateConcert
        [HttpPut("UpdateConcert")]
        public async Task<IActionResult> UpdateConcert(UpdateConcertModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _concertService.UpdateConcertAsync(model);
                return Ok("Dane koncertu zostały zaktualizowane prawidłowo");
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd podczas aktualizacji danych koncertu: {ex.Message}");
            }
        }
        #endregion
    }
}
