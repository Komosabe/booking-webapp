namespace BackednBooking.Entities
{
    public class Hall
    {
        public int Id { get; set; }
        public string HallName { get; set; }
        public int Capacity { get; set; }
        public ICollection<Concert> Concerts { get; set; }
        public int CreatedByUserId { get; set; }
        public User CreatedBy { get; set; }
    }
}
