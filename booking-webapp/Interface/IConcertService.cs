using BackendBooking.Models.Concert;

namespace BackendBooking.Interface
{
    public interface IConcertService
    {
        Task<int> CreateConcertAsync(CreateConcertModel model, string token);
        Task DeleteConcertAsync(int id);
        Task<IEnumerable<ConcertModel>> GetAllConcertsAsync(string token);
        Task<ConcertModel> GetConcertByIdAsync(int id);
        Task UpdateConcertAsync(int concertId, UpdateConcertModel model);
    }
}
