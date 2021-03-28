using EfMapping.Data.Context;
using EFMapping.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EFMapping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DefaultContext _dbContext;

        public CustomersController(DefaultContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_dbContext.Customers);
        }

        [HttpGet("{id:guid}", Name = nameof(GetById))]
        public IActionResult GetById(Guid id)
        {
            var customer = _dbContext.Customers.Find(id);
            if (customer is null)
                return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            try
            {
                _dbContext.Add(customer);
                _dbContext.SaveChanges();
                return CreatedAtRoute(nameof(GetById), new { customer.Id }, null);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
