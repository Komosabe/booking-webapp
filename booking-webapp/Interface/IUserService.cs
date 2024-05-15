using BackendBooking.Models.User;

namespace BackendBooking.Interface
{
    public interface IUserService
    {
        Task<int> CreateUserAsync(CreateUserModel model);
        Task DeleteUserAsync(int id);
        Task<IEnumerable<UserModel>> GetAllUserAsync();
        Task<UserModel> GetUserByIdAsync(int id);
        Task UpdateUserAsync(UpdateUserModel model);
        Task<bool> AuthenticateAsync(string username, string password);
    }
}
