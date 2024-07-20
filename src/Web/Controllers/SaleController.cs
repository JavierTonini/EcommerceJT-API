using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<Sale>> GetAllSales()
        {
            var sales = _saleService.GetAllSales();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Client")]
        public ActionResult<Sale> GetSaleById(int id)
        {
            try
            {
                var sale = _saleService.GetSaleById(id);
                return Ok(sale);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public ActionResult<Sale> CreateSale([FromBody] SaleDto saleDto)
        {
            var sale = _saleService.CreateSale(saleDto);
            return CreatedAtAction(nameof(GetSaleById), new { id = sale.Id }, sale);
        }

        //[HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        //public IActionResult UpdateSale(int id, [FromBody] SaleDto saleDto)
        //{
        //    try
        //    {
        //        _saleService.UpdateSale(id, saleDto);
        //        return NoContent();
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteSale(int id)
        {
            try
            {
                _saleService.DeleteSale(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
