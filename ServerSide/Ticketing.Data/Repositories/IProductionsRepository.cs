using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Ticketing.Models.Dtos;

namespace Ticketing.Data.Repositories
{
    public interface IProductionsRepository
    {
        Task<List<ProductionDto>> GetAllAvailableProductions();

        Task<ProductionDto> GetProductionById(int id);

        Task<ShowDto> GetShowById(int id);
    }
}