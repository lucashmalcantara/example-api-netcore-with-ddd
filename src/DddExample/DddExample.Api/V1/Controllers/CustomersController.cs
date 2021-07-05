using AutoMapper;
using DddExample.Api.V1.Models.CustomerContext;
using DddExample.Domain.Core.Results;
using DddExample.Domain.CustomerContext.Entities;
using DddExample.Domain.CustomerContext.Repositories;
using DddExample.Domain.CustomerContext.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DddExample.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(
            IMapper mapper,
            ICustomerService customerService,
            ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerService = customerService;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CustomerGetResultModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IReadOnlyCollection<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IReadOnlyCollection<Error>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _customerRepository.GetByIdAsync(id);

            if (result.HasError)
                return BadRequest(result.Errors);

            if (result.Data == null)
                return NoContent();

            var customerModel = _mapper.Map<Customer, CustomerGetResultModel>(result.Data);

            return Ok(customerModel);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerGetResultModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IReadOnlyCollection<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IReadOnlyCollection<Error>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _customerRepository.GetAllAsync();

            if (result.HasError)
                return BadRequest(result.Errors);

            if (result.Data == null || !result.Data.Any())
                return NoContent();

            var customerModels = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerGetResultModel>>(result.Data);

            return Ok(customerModels);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerGetResultModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IReadOnlyCollection<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IReadOnlyCollection<Error>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(CustomerPostRequestModel customerModel)
        {
            var customer = _mapper.Map<CustomerPostRequestModel, Customer>(customerModel);

            var result = await _customerService.CreateAsync(customer);

            if (result.HasError)
                return BadRequest(result.Errors);

            return Created($"/customers/{customer.Id}", customer);
        }
    }
}
