using BackendBooking.Models.Hall;

namespace BackendBooking.Interface
{
    public interface IHallService
    {
        Task<int> CreateHallAsync(CreateHallModel model);
        Task DeleteHallAsync(int id);
        Task<IEnumerable<HallModel>> GetAllHallsAsync();
        Task<HallModel> GetHallByIdAsync(int id);
        Task UpdateHallAsync(int hallId, UpdateHallModel model);
    }
}
