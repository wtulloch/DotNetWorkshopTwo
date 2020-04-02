using System.Collections.Generic;

namespace Ticketing.Models.Dtos
{
    public class ShowDto
    {
        public int Id { get; set; }
        public string ProductionName { get; set; }
        public string ShowDate { get; set; }

        public List<TicketDto> Tickets { get; set; }
        
    }
}