using BackendBooking.Authorization;
using BackendBooking.Interface;
using BackendBooking.Models.Reservation;
using Microsoft.AspNetCore.Mvc;

namespace BackendBooking.Controllers.Reservation
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        #region CreateReservation
        [HttpPost("CreateReservation")]
        public async Task<IActionResult> CreateReservation(CreateReservationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var reservationId = await _reservationService.CreateReservationAsync(model, token);
                return Ok($"Rezerwacja została utworzona pomyślnie. Id: {reservationId}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd podczas tworzenia rezerwacji: {ex.Message}");
            }
        }
        #endregion

        #region DeleteReservation
        [HttpDelete("DeleteReservation/{reservationId}")]
        public async Task<IActionResult> DeleteReservation(int reservationId)
        {
            try
            {
                await _reservationService.DeleteReservationAsync(reservationId);
                return Ok("Rezerwacja została usunięta pomyślnie.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd podczas usuwania rezerwacji: {ex.Message}");
            }
        }
        #endregion

        #region GetReservationsForConcert
        [HttpGet("GetReservationsForConcert/{concertId}")]
        public async Task<IActionResult> GetReservationsForConcert(int concertId)
        {
            try
            {
                var reservations = await _reservationService.GetReservationsForConcertAsync(concertId);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd podczas pobierania rezerwacji dla koncertu: {ex.Message}");
            }
        }
        #endregion

        #region GetReservationsForUser
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

        #endregion
    }
}
