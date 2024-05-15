namespace BackednBooking.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int ConcertId { get; set; }
        public Concert Concert { get; set; } 
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
