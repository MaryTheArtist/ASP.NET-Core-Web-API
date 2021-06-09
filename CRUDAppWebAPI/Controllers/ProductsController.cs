using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Authentication;

namespace CRUDAppWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IDataRepository<Product> _dataRepository;

        public ProductsController(IDataRepository<Product> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Product> products = _dataRepository.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id)
        {
            Product product = _dataRepository.Get(id);
            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null.");
            }
            _dataRepository.Add(product);
            return CreatedAtRoute("Get", new { id = product.ProductID }, product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Put(string id, [FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null.");

            }
            Product productToUpdate = _dataRepository.Get(id);
            if (productToUpdate == null)
            {
                return NotFound("The Product record couldn't be found.");
            }
            _dataRepository.Update(productToUpdate, product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public IActionResult Delete(string id)
        {
            Product product = _dataRepository.Get(id);
            if (product == null)
            {
                return NotFound("The Product record couldn't be found.");
            }
            _dataRepository.Delete(product);
            return NoContent();
        }
    }
}
