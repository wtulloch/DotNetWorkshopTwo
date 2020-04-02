using System;
using System.Collections.Generic;

namespace Ticketing.Models.Dtos
{
    public class ProductionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public List<ShowSummaryDto> Shows { get; set; }

    }
}