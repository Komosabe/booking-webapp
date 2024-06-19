using AutoMapper;
using BackednBooking.Entities;
using BackednBooking.Helpers;
using BackendBooking.Authorization;
using BackendBooking.Interface;
using BackendBooking.Models.Reservation;
using Microsoft.EntityFrameworkCore;

namespace BackendBooking.Service
{
    public class ReservationService : IReservationService
    {
        private readonly BookingDbContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public ReservationService(BookingDbContext context, IJwtUtils jwtUtils, IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        #region CreateReservationAsync
        public async Task<int> CreateReservationAsync(CreateReservationModel model, string token)
        {
            try
            {
                var userId = _jwtUtils.ValidateToken(token);
                if (userId == null)
                    throw new UnauthorizedAccessException("Unauthorized");

                model.UserId = userId.Value;

                var concert = await _context.Concerts.FindAsync(model.ConcertId);
                if (concert == null)
                    throw new KeyNotFoundException($"Koncert o identyfikatorze {model.ConcertId} nie istnieje.");

                var user = await _context.Users.FindAsync(model.UserId);
                if (user == null)
                    throw new KeyNotFoundException($"Użytkownik o identyfikatorze {model.UserId} nie istnieje.");

                var reservationEntity = _mapper.Map<Reservation>(model);

                _context.Reservations.Add(reservationEntity);
                await _context.SaveChangesAsync();

                return reservationEntity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Wystąpił błąd podczas tworzenia rezerwacji.", ex);
            }
        }
        #endregion

        #region DeleteReservation
        public async Task DeleteReservationAsync(int reservationId)
        {
            try
            {
                var reservation = await _context.Reservations.FindAsync(reservationId);
                if (reservation == null)
                {
                    throw new KeyNotFoundException($"Rezerwacja o identyfikatorze {reservationId} nie istnieje.");
                }

                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Wystąpił błąd podczas usuwania rezerwacji.", ex);
            }
        }
        #endregion

        #region GetAllReservationForConcert
        public async Task<IEnumerable<ReservationModel>> GetReservationsForConcertAsync(int concertId)
        {
            try
            {
                var reservations = await _context.Reservations
                    .Where(r => r.ConcertId == concertId)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<ReservationModel>>(reservations);
            }
            catch (Exception ex)
            {
                throw new Exception($"Wystąpił błąd podczas pobierania rezerwacji dla koncertu o identyfikatorze {concertId}.", ex);
            }
        }
        #endregion

        #region GetReservationForUserAsync
        public async Task<IEnumerable<ReservationModel>> GetReservationsForUserAsync(string token)
        {
            try
            {
                var userId = _jwtUtils.ValidateToken(token);
                if (userId == null)
                    throw new UnauthorizedAccessException("Unauthorized");

                var reservations = await _context.Reservations
                    .Where(r => r.UserId == userId)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<ReservationModel>>(reservations);
            }
            catch (Exception ex)
            {
                throw new Exception("Błąd podczas pobierania rezerwacji dla użytkownika.", ex);
            }
        }
        #endregion
    }
}
