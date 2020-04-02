using System.Collections.Generic;

namespace Ticketing.Models.DbModels
{
    public partial class Theatre
    {
        public Theatre()
        {
            Productions = new HashSet<Production>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SeatsAvailable { get; set; }
        public string TicketPrefix { get; set; }

        public virtual ICollection<Production> Productions { get; set; }
    }
}
