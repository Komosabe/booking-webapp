﻿namespace BackendBooking.Models.Concert
{
    public class UpdateConcertModel
    {
        //public int Id { get; set; }
        public string ConcertName { get; set; }
        public string ArtistName { get; set; }
        public DateTime DateTime { get; set; }
        public int HallId { get; set; }
    }
}
