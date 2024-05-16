using BackednBooking.Entities;
using BackendBooking.Models.User;

namespace BackendBooking.Interface
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        void Register(RegisterRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        GetModelUser GetViewById(int id);
        void Update(int id, UpdateUserRequest model);
        void Delete(int id);
    }
}
