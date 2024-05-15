using BackednBooking.Entities;

namespace BackendBooking.Models.User
{
    public class UpdateUserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
