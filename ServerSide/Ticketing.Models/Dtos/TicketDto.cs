namespace Ticketing.Models.Dtos
{
    public class TicketDto
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public int? UserId { get; set; }
        public string TicketNumber { get; set; }
        public bool Purchased { get; set; }
    }
}