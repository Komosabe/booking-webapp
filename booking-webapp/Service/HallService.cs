using AutoMapper;
using BackednBooking.Entities;
using BackednBooking.Helpers;
using BackendBooking.Authorization;
using BackendBooking.Interface;
using BackendBooking.Models.Concert;
using BackendBooking.Models.Hall;
using Microsoft.EntityFrameworkCore;
using System;

namespace BackendBooking.Service
{
    public class HallService : IHallService
    {
        private readonly BookingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IJwtUtils _jwtUtils;

        public HallService(BookingDbContext context, IMapper mapper, IJwtUtils jwtUtils)
        {
            _context = context;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
        }

        #region CreateHall
        public async Task<int> CreateHallAsync(CreateHallModel model, string token)
        {
            try
            {
                var userId = _jwtUtils.ValidateToken(token);
                if (userId == null)
                    throw new UnauthorizedAccessException("Unauthorized");

                model.CreatedByUserId = userId.Value;

                var hallEntity = _mapper.Map<Hall>(model);

                _context.Halls.Add(hallEntity);
                await _context.SaveChangesAsync();

                return hallEntity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Błąd podczas dodawania sali", ex);
            }
        }
        #endregion

        #region DeleteHall
        public async Task DeleteHallAsync(int id)
        {
            try
            {
                var hall = await _context.Halls.FindAsync(id);

                if (hall == null)
                    throw new Exception("Sala o podanym identyfikatorze nie istnieje");

                _context.Halls.Remove(hall);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Błąd podczas usuwania sali", ex);
            }
        }
        #endregion

        #region GetAllHalls
        public async Task<IEnumerable<HallModel>> GetAllHallsAsync(string token)
        {
            try
            {
                var userId = _jwtUtils.ValidateToken(token);
                if (userId == null)
                    throw new UnauthorizedAccessException("Unauthorized");

                var halls = await _context.Halls.Include(h => h.CreatedBy).ToListAsync();
                var hallModels = _mapper.Map<IEnumerable<HallModel>>(halls);

                foreach (var hallModel in hallModels)
                {
                    var hall = halls.First(h => h.Id == hallModel.Id);
                    hallModel.IsEditable = hall.CreatedByUserId == userId;
                }

                return hallModels;
            }
            catch (Exception ex)
            {
                throw new Exception("Błąd podczas pobierania wszystkich sal", ex);
            }
        }
        #endregion

        #region GetHallById
        public async Task<HallModel> GetHallByIdAsync(int id)
        {
            try
            {
                var hall = await _context.Halls.FindAsync(id);

                if (hall == null)
                    throw new Exception("Sala o podanym identyfikatorze nie istnieje");

                return _mapper.Map<HallModel>(hall);
            }
            catch (Exception ex)
            {
                throw new Exception("Błąd podczas pobierania sali o określonym identyfikatorze", ex);
            }
        }
        #endregion

        #region UpdateHall
        public async Task UpdateHallAsync(int hallId, UpdateHallModel model)
        {
            try
            {
                var hall = await _context.Halls.FindAsync(hallId);

                if (hall == null)
                    throw new Exception("Sala o podanym identyfikatorze nie istnieje");

                _mapper.Map(model, hall);
                _context.Halls.Update(hall);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Błąd podczas aktualizacji danych sali", ex);
            }
        }
        #endregion
    }
}
