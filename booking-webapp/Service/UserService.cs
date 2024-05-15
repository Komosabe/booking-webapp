using AutoMapper;
using BackednBooking.Entities;
using BackednBooking.Helpers;
using BackendBooking.Interface;
using BackendBooking.Models.User;
using Microsoft.EntityFrameworkCore;
using System;

namespace BackendBooking.Service
{
    public class UserService : IUserService
    {
        private readonly BookingDbContext _context;
        private readonly IMapper _mapper;

        public UserService(BookingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region CreateUserAsync
        public async Task<int> CreateUserAsync(CreateUserModel model)
        {
            try
            {
                var userEntity = _mapper.Map<User>(model);

                _context.Users.Add(userEntity);

                await _context.SaveChangesAsync();

                return userEntity.Id;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Błąd podczas aktualizacji bazy danych", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Wystąpił nieoczekiwany błąd", ex);
            }
        }
        #endregion

        #region DeleteUserAsync
        public async Task DeleteUserAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                    throw new Exception("Użytkownik o podantym identyfikatorze nie istnieje");

                _context.Users.Remove(user);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Wystąpił błąd podczas usuwania użytkownika: ", ex);
            }
        }
        #endregion

        #region GetAllUsersAsync
        public async Task<IEnumerable<UserModel>> GetAllUserAsync()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return _mapper.Map<IEnumerable<UserModel>>(users);
            }
            catch (Exception ex)
            {
                throw new Exception("Wystąpił błąd podczas pobierania wszystkich użytkowników: ", ex);
            }
            
        }
        #endregion

        #region GetUserById
        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                    throw new Exception("Użytkownik o podanym identyfikatorze nie istnieje");

                return _mapper.Map<UserModel>(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Wystąpił błąd podczas pobierania użytkownika", ex);
            }
        }
        #endregion

        #region UpdateUser
        public async Task UpdateUserAsync(UpdateUserModel model)
        {
            try
            {
                var user = await _context.Users.FindAsync(model.Id);

                if (user == null)
                    throw new Exception("Użytkownik o podanym identyfikatorze nie istnieje");

                _mapper.Map(model, user);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Wystąpił błąd podczas aktualizacji danych użytkownika", ex);
            }
        }
        #endregion

        #region Login
        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
                return user != null;
            }
            catch (Exception ex)
            {
                throw new Exception("Błąd podczas autentykacji użytkownika.", ex);
            }
        }
        #endregion
    }
}
