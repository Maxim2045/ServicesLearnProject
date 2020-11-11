using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PriceService.Models;
using PriceService.Repository;

namespace PriceService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly IPriceRepository _priceRepository;
        private readonly IMapper _mapper;

        public PriceController(IPriceRepository priceRepository, IMapper mapper)
        {
            _priceRepository = priceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Price>> GetAll()
        {
            var priceEntity = await _priceRepository.GetAll();
            return _mapper.Map<IEnumerable<Price>>(priceEntity);
        }

        [HttpGet("{id}")]
        public async Task<Price> GetById(Guid id)
        {
            var priceEntity = await _priceRepository.GetById(id);
            return _mapper.Map<Price>(priceEntity);
        }

        [HttpPost]
        public async Task Create(Price price)
        {
            var priceEntity = _mapper.Map<PriceDbModel>(price);
            await _priceRepository.Create(priceEntity);
        }

        [HttpPut]
        public async Task Update(Price price)
        {
            var priceEntity = _mapper.Map<PriceDbModel>(price);
            await _priceRepository.Update(priceEntity);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _priceRepository.Delete(id);
        }


    }
}
