using System;

namespace BackednBooking.Entities
{
    public class Concert
    {
        public int Id { get; set; }
        public string ConcertName { get; set; }
        public string ArtistName { get; set; }
        public DateTime DateTime { get; set; }
        public int HallId { get; set; }
        public Hall Hall { get; set; } 
        public ICollection<Reservation> Reservations { get; set; } 
    }
}
