namespace Ticketing.Models.DbModels
{
    public partial class UserTicket
    {
        public int UserId { get; set; }
        public int TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }
        public virtual User User { get; set; }
    }
}
