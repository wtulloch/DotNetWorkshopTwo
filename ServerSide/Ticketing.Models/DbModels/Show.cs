using System;
using System.Collections.Generic;

namespace Ticketing.Models.DbModels
{
    public partial class Show
    {
        public Show()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public int ProductionId { get; set; }
        public DateTime ShowDate { get; set; }

        public virtual Production Production { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
