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

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>All products</returns>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Product> products = _dataRepository.GetAll();
            return Ok(products);
        }

        /// <summary>
        /// Get product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id)
        {
            Product product = _dataRepository.Get(id);
            return Ok(product);
        }
        
        /// <summary>
        /// Create product
        /// You can create product if your role is Admin or SuperAdmin
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Created product</returns>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public IActionResult Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null.");
            }
            _dataRepository.Add(product);
            return CreatedAtRoute("Get", new { id = product.ProductID }, product);
        }

        /// <summary>
        /// Update product by ID
        /// You can update product if your role is Admin or SuperAdmin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        [Authorize(Roles = UserRoles.SuperAdmin)]
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

        /// <summary>
        /// Delete product by ID
        /// Only Super Admin can delete products
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
