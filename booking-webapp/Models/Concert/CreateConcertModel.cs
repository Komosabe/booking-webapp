namespace BackendBooking.Models.Concert
{
    public class CreateConcertModel
    {
        public string ConcertName { get; set; }
        public string ArtistName { get; set; }
        public DateTime DateTime { get; set; }
        public int HallId { get; set; }
        public int CreatedByUserId { get; set; }
    }
}
