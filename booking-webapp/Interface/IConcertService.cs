using BackendBooking.Models.Concert;

namespace BackendBooking.Interface
{
    public interface IConcertService
    {
        Task<int> CreateConcertAsync(CreateConcertModel model);
        Task DeleteConcertAsync(int id);
        Task<IEnumerable<ConcertModel>> GetAllConcertsAsync();
        Task<ConcertModel> GetConcertByIdAsync(int id);
        Task UpdateConcertAsync(int concertId, UpdateConcertModel model);
    }
}
