using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RetailInMotion.Core;
using RetailInMotion.Core.Entities;
using RetailInMotion.Core.Interfaces;

namespace RetailInMotion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Gets a List asynchronously of all products
        /// </summary>
        /// <returns></returns>
        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productRepository.ListAllAsync();
            return Ok(products);
        }

        /// <summary>
        /// Gets a product asynchronously by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return Ok(product);
        }

        /// <summary>
        /// Creates asynchronously a product on db
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        // POST: api/Product
        [HttpPost("registerproduct")]
        public async Task<IActionResult> Post(Product product)
        {
            if(product == null)
                return BadRequest("Product information missing");

            await _productRepository.CreateAsync(product);

            return Ok();
        }
        /// <summary>
        /// Updates asynchronously a product on db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
           
            await _productRepository.UpdateAsync(product);
            return Ok();
        }
        /// <summary>
        /// Deletes asynchronously a product based on its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if(product == null)
                return NotFound();

            await _productRepository.DeleteAsync(product);

            return NoContent();
        }

        /// <summary>
        /// Gets a paginated list of products
        /// This endpoint uses the parameters passed using url 
        /// e.g of url http://localhost:5000/api/product/paginatedlist?pageNumber=1&pageSize=5
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet("paginatedlist")]
        public async Task<IActionResult> GetPaginated([FromQuery]ProductPagingModel parameters)
        {
            var products = await _productRepository.ListAllAsync();
            var productsPaginated = _productRepository.ListPaginated(parameters.pageNumber,parameters.pageSize);
            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = parameters.pageNumber > 1 ? "Yes" : "No";  
            var TotalPages = (int)Math.Ceiling(products.Count / (double)parameters.pageSize);
            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = parameters.pageNumber < (int)Math.Ceiling(products.Count / (double)parameters.pageSize) ? "Yes" : "No";  
            var paginationMetadata = new  
            {  
                totalCount = products.Count,  
                pageSize = parameters.pageSize,  
                currentPage = parameters.pageNumber,  
                totalPages = TotalPages,  
                previousPage,  
                nextPage  
            };  

            // Setting Header  
            HttpContext.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject (paginationMetadata));
            return Ok(productsPaginated);
        }
    }
}
