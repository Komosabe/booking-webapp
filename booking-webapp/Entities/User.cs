using System.Text.Json.Serialization;

namespace BackednBooking.Entities
{
    public class User
    {
        public int Id { get; set; } 
        public string Username { get; set; } 


        [JsonIgnore]
        public string PasswordHash { get; set; }
        public ICollection<Reservation>? Reservations { get; set; } 
    }
}
