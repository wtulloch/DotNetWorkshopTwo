using System;
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
        public async Task<List<ProductionDto>> GetAllAvailableProductions()
        {
            var currentProductions = await _ticketingContext.Productions
                .Include(p => p.Shows)
                .Where(p => p.Shows.Any(s => s.ShowDate >= DateTime.Now))
                .ToListAsync();


            var productionsToReturn = _mapper.Map<List<ProductionDto>>(currentProductions);
            foreach (var production in productionsToReturn)
            {
                SetProductionStartAndEnd(production);
            }

            return productionsToReturn;
        }

       

        public async Task<ProductionDto> GetProductionById(int id)
        {
            var production = await _ticketingContext.Productions
                .Where(p => p.Id == id)
                .Include(p => p.Shows)
                .FirstOrDefaultAsync();

            var productionDto = _mapper.Map<ProductionDto>(production);
            SetProductionStartAndEnd(productionDto);

            return productionDto;
        }

        public async Task<ShowDto> GetShowById(int id)
        {
            var show = await _ticketingContext.Shows
                .Include(s => s.Production)
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(s => s.Id == id);

            var showDto = _mapper.Map<ShowDto>(show);

            return showDto;
        }

        private static void SetProductionStartAndEnd(ProductionDto production)
        {
            production.StartDate = production.Shows.Min(s => s.ShowDate);
            production.EndDate = production.Shows.Max(s => s.ShowDate);
        }
    }
}