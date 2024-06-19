namespace BackendBooking.Models.Hall
{
    public class CreateHallModel
    {
        public string HallName { get; set; }
        public int Capacity { get; set; }
        public int CreatedByUserId { get; set; }
    }
}
