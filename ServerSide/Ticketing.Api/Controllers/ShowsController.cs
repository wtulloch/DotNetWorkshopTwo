using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Data.Repositories;
using Ticketing.Models.Dtos;

namespace Ticketing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IProductionsRepository _repository;

        public ShowsController(IProductionsRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ShowDto>> GetShow(int id)
        {
            return await _repository.GetShowById(id);
        }
    }
}