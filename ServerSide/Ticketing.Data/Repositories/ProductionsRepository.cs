using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ticketing.Models.Dtos;

namespace Ticketing.Data.Repositories
{
    public class ProductionsRepository : IProductionsRepository
    {
        private readonly TicketingDbContext _ticketingContext;
        private readonly IMapper _mapper;

        public ProductionsRepository(TicketingDbContext ticketingContext, IMapper mapper)
        {
            _ticketingContext = ticketingContext;
            _mapper = mapper;
        }
        public Task<List<ProductionDto>> GetAllAvailableProductions()
        {
            var currentProductions = _ticketingContext.Productions.Include(p => p.Shows);

            var productionsToReturn = _mapper.Map<List<ProductionDto>>(currentProductions);
            foreach (var production in productionsToReturn)
            {

                production.StartDate = production.Shows.Min(s => s.ShowDate);
                production.EndDate = production.Shows.Max(s => s.ShowDate);
            }

            return Task.FromResult(productionsToReturn);

        }
    }
}