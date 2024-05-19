namespace BackendBooking.Models.Reservation
{
    public class CreateReservationModel
    {
        public int ConcertId { get; set; }
        public int? UserId { get; set; }
    }
}
