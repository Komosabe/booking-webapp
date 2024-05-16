using BackednBooking.Entities;

namespace BackendBooking.Models.User
{
    public class GetModelUser
    {
        public int Id { get; set; }
        public string Username { get; set; }


        public ICollection<BackednBooking.Entities.Reservation>? Reservations { get; set; }
    }
}
