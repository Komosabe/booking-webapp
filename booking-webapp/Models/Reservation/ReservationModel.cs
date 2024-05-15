namespace BackendBooking.Models.Reservation
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public int ConcertId { get; set; }
        public int UserId { get; set; }
    }
}
