using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] ProductCreateRequest productCreateRequest)
        {
            var newObj = _productService.Create(productCreateRequest);
            return CreatedAtAction(nameof(Get), new { id = newObj.Id }, newObj);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProductUpdateRequest productUpdateRequest)
        {
            try
            {
                _productService.Update(id, productUpdateRequest);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _productService.Delete(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<Product>> GetAll()
        {
            return _productService.GetAll();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<Product> Get([FromRoute] int id)
        {
            try
            {
                return _productService.GetById(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<Product>> GetAllIncludingInactive()
        {
            return Ok(_productService.GetAllIncludingInactive());
        }
    }
}
