namespace Ticketing.Models.DbModels
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public int? UserId { get; set; }
        public string TicketNumber { get; set; }
        public bool Purchased { get; set; }

        public virtual Show Show { get; set; }
    }
}
