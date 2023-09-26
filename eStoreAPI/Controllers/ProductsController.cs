using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public ActionResult<IEnumerable<ProductResponseDTO>> Get()
        {
            return _productRepository.GetProducts();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public ActionResult<ProductDTO?> Get(int id)
        {
            var result = _productRepository.GetProduct(id);
            if(result != null)
            {
                return result;
            } else
            {
                return NotFound();
            }
        }

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductDTO p)
        {
            if (ModelState.IsValid)
            {
                bool result = _productRepository.SaveProduct(p);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductDTO p)
        {
            var tempProduct = _productRepository.GetProduct(id);
            if (tempProduct == null)
            {
                return NotFound();
            }
            _productRepository.UpdateProduct(p);
            return NoContent();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tempProduct = _productRepository.GetProduct(id);
            if (tempProduct == null)
            {
                return NotFound();
            }
            bool result = _productRepository.DeleteProduct(tempProduct);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
