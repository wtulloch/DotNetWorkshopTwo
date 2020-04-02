using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Models.Dtos;

namespace Ticketing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketingController : ControllerBase
    {

        [HttpPut("Purchase")]
        public async Task<IActionResult> PurchaseTickets(List<TicketDto> tickets)
        {
            return new OkResult();
        }
    }
}