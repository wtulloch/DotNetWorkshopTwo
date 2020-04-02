using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ticketing.Data;
using Ticketing.Data.Repositories;
using Ticketing.Models.Dtos;

namespace Ticketing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionsController : ControllerBase
    {
        private readonly IProductionsRepository _productionsRepository;


        public ProductionsController(IProductionsRepository productionsRepository)
        {
            _productionsRepository = productionsRepository;
        }

        [HttpGet]
        public async Task<List<ProductionDto>> Productions()
        {

            return await _productionsRepository.GetAllAvailableProductions();


        }
    }
    
}