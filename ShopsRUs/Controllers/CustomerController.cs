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
    public class CustomerController : ControllerBase
    {
        private readonly ICostumerRepository _repository;
        private readonly IMapper _mapper;
        private readonly int _perPage;


        public CustomerController(ICostumerRepository repository, IMapper mapper ,IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _perPage = Convert.ToInt32(configuration.GetSection("PaginationSettings:perPage").Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers([FromQuery] int page)
        {

            page = page <= 0 ? 1 : page;

            var customers = await _repository.GetAllCustomerAsync(page, _perPage);

            var customerToReturn = _mapper.Map<IEnumerable<UserReadDto>>(customers);


            var pageMetaData = Utilities.Paginate(page, _perPage, _repository.TotalCount);
            var pagedItems = new PaginatedResultDto<UserReadDto> { PageMetaData = pageMetaData, ResponseData = customerToReturn };
            return Ok(Utilities.CreateResponse(message: "All Customers", errs: null, data: pagedItems));

        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(UserToAddDto user)
        {

            if (!ModelState.IsValid || string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
            {
                return BadRequest(Utilities.CreateResponse(message: "Model state error", errs: ModelState, data: ""));
            }

            var customer = _mapper.Map<User>(user);

            var customerAdded = await _repository.CreateCustomerAsync(customer);

            if (customerAdded == false)
            {
                ModelState.AddModelError("Customer", "Could not add category");
                return BadRequest(Utilities.CreateResponse(message: "Customer not added", errs: ModelState, data: ""));
            }

            return Ok(Utilities.CreateResponse(message: "Added sucessfully", errs: null, data: ""));

        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCustomerById(string userId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(Utilities.CreateResponse(message: "Model state error", errs: ModelState, data: ""));
            }

            var result = await _repository.GetCustomerByIdAsync(userId);
            var customer = _mapper.Map<UserReadDto>(result);


            if (result == null)
            {
                ModelState.AddModelError("Customer", "Could not get customer");
                return NotFound(Utilities.CreateResponse(message: "Customer not found", errs: ModelState, data: customer));
            }

            return Ok(Utilities.CreateResponse(message: "New customer added", errs: null, data: customer));

        }

        [HttpGet("get-by-name")]
        public async Task<IActionResult> GetCustomerByName(string name)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(Utilities.CreateResponse(message: "Model state error", errs: ModelState, data: ""));
            }

            var result = await _repository.GetCustomerByNameAsync(name); 

            if (result.Count() == 0)
            {
                ModelState.AddModelError("Customer", "Could not get customer");
                return NotFound(Utilities.CreateResponse(message: "Customer not found", errs: ModelState, data: ""));
            }

            var customer = _mapper.Map<IEnumerable<UserReadDto>>(result);

            return Ok(Utilities.CreateResponse(message: "Customer(s) from database", errs: null, data: customer));

        }
    }
}
