using System.Collections.Generic;

namespace Ticketing.Models.DbModels
{
    public partial class Production
    {
        public Production()
        {
            Shows = new HashSet<Show>();
        }

        public int Id { get; set; }
        public int TheatreId { get; set; }
        public string Name { get; set; }
        public string TicketSuffix { get; set; }

        public virtual Theatre Theatre { get; set; }
        public virtual ICollection<Show> Shows { get; set; }
    }
}
