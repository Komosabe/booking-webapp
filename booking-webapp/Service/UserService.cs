using AutoMapper;
using BackednBooking.Entities;
using BackednBooking.Helpers;
using BackendBooking.Authorization;
using BackendBooking.Helpers;
using BackendBooking.Interface;
using BackendBooking.Models.User;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BackendBooking.Service
{
    public class UserService : IUserService
    {
        private BookingDbContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public UserService(
            BookingDbContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        #region Authenticate
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

            // validate
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }
        #endregion

        #region Register
        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.Users.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            // map model to new user object
            var user = _mapper.Map<User>(model);

            // hash password

            //user.PasswordHash = BCrypt.HashPassword(model.Password);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            // save user
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        #endregion

        #region GetAllUsers
        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }
        #endregion

        #region GetUserById
        public User GetById(int id)
        {
            return getUser(id);
        }
        #endregion

        #region GetUserViewByid
        public GetModelUser GetViewById(int id)
        {
            var user = _context.Users
                    .Include(a => a.Reservations)
                    .FirstOrDefault(b => b.Id == id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return _mapper.Map<GetModelUser>(user);
        }
        #endregion

        #region getUser
        private User getUser(int id)
        {
            var user = _context.Users
                    .Include(b => b.Reservations)
                    .FirstOrDefault(b => b.Id == id);

            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
        #endregion

        #region UpdateUser
        public void Update(int id, UpdateUserRequest model)
        {
            var user = getUser(id);

            // validate
            if (model.Username != user.Username && _context.Users.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // copy model to user and save
            _mapper.Map(model, user);
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        #endregion

        #region DeleteUser
        public void Delete(int id)
        {
            var user = getUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        #endregion

        #region GetCurrentUserDto
        public UserMe GetCurrentUserDto(int id)
        {
            var user = getUserMe(id);
            return _mapper.Map<UserMe>(user);
        }
        #endregion

        #region getUser
        private User getUserMe(int id)
        {
            var user = _context.Users
                    .Include(b => b.Reservations)
                    .FirstOrDefault(b => b.Id == id);

            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
        #endregion
    }
}
