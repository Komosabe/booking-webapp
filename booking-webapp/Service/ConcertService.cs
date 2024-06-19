using AutoMapper;
using BackednBooking.Entities;
using BackednBooking.Helpers;
using BackendBooking.Authorization;
using BackendBooking.Interface;
using BackendBooking.Models.Concert;
using Microsoft.EntityFrameworkCore;

namespace BackendBooking.Service
{
    public class ConcertService : IConcertService
    {
        private readonly BookingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IJwtUtils _jwtUtils;

        public ConcertService(BookingDbContext context, IMapper mapper, IJwtUtils jwtUtils)
        {
            _context = context;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
        }

        #region CreateConcert
        public async Task<int> CreateConcertAsync(CreateConcertModel model, string token)
        {
            try
            {
                var userId = _jwtUtils.ValidateToken(token);
                if (userId == null)
                    throw new UnauthorizedAccessException("Unauthorized");

                model.CreatedByUserId = userId.Value;

                var concertEntity = _mapper.Map<Concert>(model);

                _context.Concerts.Add(concertEntity);
                await _context.SaveChangesAsync();

                return concertEntity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Błąd podczas dodawania koncertu", ex);
            }
        }
        #endregion

        #region DeleteConcert
        public async Task DeleteConcertAsync(int id)
        {
            try
            {
                var concert = await _context.Concerts.FindAsync(id);

                if (concert == null)
                    throw new Exception("Koncert o podanym identyfikatorze nie istnieje");

                _context.Concerts.Remove(concert);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Błąd podczas usuwania koncertu", ex);
            }
        }
        #endregion

        #region GetAllConcerts
        public async Task<IEnumerable<ConcertModel>> GetAllConcertsAsync(string token)
        {
            try
            {
                var userId = _jwtUtils.ValidateToken(token);
                if (userId == null)
                    throw new UnauthorizedAccessException("Unauthorized");

                var concerts = await _context.Concerts
                    .Include(c => c.CreatedBy)
                    .Include(c => c.Hall)
                    .Include(c => c.Reservations)
                    .ToListAsync();

                var concertModels = _mapper.Map<IEnumerable<ConcertModel>>(concerts);

                foreach (var concertModel in concertModels)
                {
                    var concert = concerts.First(c => c.Id == concertModel.Id);
                    concertModel.IsEditable = concert.CreatedByUserId == userId;
                    concertModel.MaxReservations = concert.Hall.Capacity;
                    concertModel.CurrentReservations = concert.Reservations?.Count(r => r.ConcertId == concert.Id) ?? 0;
                }

                return concertModels;
            }
            catch (Exception ex)
            {
                throw new Exception("Błąd podczas pobierania wszystkich koncertów", ex);
            }
        }
        #endregion


        #region GetConcertById
        public async Task<ConcertModel> GetConcertByIdAsync(int id)
        {
            try
            {
                var concert = await _context.Concerts.FindAsync(id);

                if (concert == null)
                    throw new Exception("Koncert o podanym identyfikatorze nie istnieje");

                return _mapper.Map<ConcertModel>(concert);
            }
            catch (Exception ex)
            {
                throw new Exception("Błąd podczas pobierania koncertu o określonym identyfikatorze", ex);
            }
        }
        #endregion

        #region UpdateConcert
        public async Task UpdateConcertAsync(int concertId, UpdateConcertModel model)
        {
            try
            {
                var concert = await _context.Concerts.FindAsync(concertId);

                if (concert == null)
                    throw new Exception("Koncert o podanym identyfikatorze nie istnieje");

                _mapper.Map(model, concert);
                _context.Concerts.Update(concert);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Błąd podczas aktualizacji danych koncertu", ex);
            }
        }
        #endregion
    }
}
