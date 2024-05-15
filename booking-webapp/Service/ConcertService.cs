using AutoMapper;
using BackednBooking.Entities;
using BackednBooking.Helpers;
using BackendBooking.Interface;
using BackendBooking.Models.Concert;
using Microsoft.EntityFrameworkCore;

namespace BackendBooking.Service
{
    public class ConcertService : IConcertService
    {
        private readonly BookingDbContext _context;
        private readonly IMapper _mapper;

        public ConcertService(BookingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region CreateConcert
        public async Task<int> CreateConcertAsync(CreateConcertModel model)
        {
            try
            {
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
        public async Task<IEnumerable<ConcertModel>> GetAllConcertsAsync()
        {
            try
            {
                var concerts = await _context.Concerts.ToListAsync();
                return _mapper.Map<IEnumerable<ConcertModel>>(concerts);
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
        public async Task UpdateConcertAsync(UpdateConcertModel model)
        {
            try
            {
                var concert = await _context.Concerts.FindAsync(model.Id);

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
