namespace BackendBooking.Models.Concert
{
    public class ConcertModel
    {
        public int Id { get; set; }
        public string ConcertName { get; set; }
        public string ArtistName { get; set; }
        public DateTime DateTime { get; set; }
        public int HallId { get; set; }
        public bool IsEditable { get; set; }
        public int MaxReservations { get; set; }
        public int CurrentReservations { get; set; }
    }
}
