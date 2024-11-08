namespace SzallodaWebApi.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public DateTime ArriveDate { get; set; }
        public DateTime DepartureDate { get; set; }

        public virtual Guest Guest { get; set; }
        public virtual Room Room { get; set; }
    }
}
