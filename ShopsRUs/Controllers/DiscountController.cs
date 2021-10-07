using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShopsRUs.Dtos;
using ShopsRUs.Helpers;
using ShopsRUs.Models;
using ShopsRUs.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountsRepository _repository;
        private readonly IMapper _mapper;
        private readonly int _perPage;


        public DiscountController(IDiscountsRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _perPage = Convert.ToInt32(configuration.GetSection("PaginationSettings:perPage").Value);

        }


        [HttpGet]
        public async Task<IActionResult> GetDiscounts([FromQuery] int page)
        {
            page = page <= 0 ? 1 : page;

            var discount = await _repository.GetAllDiscountAsync(page, _perPage);

            var discountToReturn = _mapper.Map<IEnumerable<DiscountReadDto>>(discount);


            var pageMetaData = Utilities.Paginate(page, _perPage, _repository.TotalCount);

            var pagedItems = new PaginatedResultDto<DiscountReadDto> { PageMetaData = pageMetaData, ResponseData = discountToReturn };
            return Ok(Utilities.CreateResponse(message: "All Customers", errs: null, data: pagedItems));
        }


        [HttpPost]
        public async Task<IActionResult> AddDiscounts(DiscountAddDto discount)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(Utilities.CreateResponse(message: "Model state error", errs: ModelState, data: ""));
            }

            var discountToAdd = new Discount { DiscountRate = discount.DiscountRate };

            var discountAdded = await _repository.AddDiscount(discountToAdd, discount.UserType);

            if (discountAdded == false)
            {
                ModelState.AddModelError("Discount", "Could not add new discount");
                return BadRequest(Utilities.CreateResponse(message: "Discount not added", errs: ModelState, data: ""));
            }

            return Ok(Utilities.CreateResponse(message: "Added sucessfully", errs: null, data: ""));

        }


        [HttpGet("{discountType}")]
        public async Task<IActionResult> GetDiscountByPercentageType(string discountType)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(Utilities.CreateResponse(message: "Model state error", errs: ModelState, data: ""));
            }

            var result = await _repository.GetDiscountPercentageByTypeAsync(discountType);
            var discount = _mapper.Map<DiscountReadDto>(result);


            if (result == null)
            {
                ModelState.AddModelError("Discount", "Could not get discount");
                return NotFound(Utilities.CreateResponse(message: "Discount not found", errs: ModelState, data: discount));
            }

            return Ok(Utilities.CreateResponse(message: "New disccount added", errs: null, data: discount));

        }

    }
}
