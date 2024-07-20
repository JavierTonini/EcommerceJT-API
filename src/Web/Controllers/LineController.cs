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
    public class LineController : ControllerBase
    {
        private readonly ILineService _lineService;

        public LineController(ILineService lineService)
        {
            _lineService = lineService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<LineGetRequest>> GetAllLines()
        {
            var lines = _lineService.GetAllLines();
            return Ok(lines);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<LineGetRequest> GetLineById(int id)
        {
            try
            {
                var line = _lineService.GetLineById(id);
                return Ok(line);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public ActionResult<Line> CreateLine([FromBody] LineCreateRequest lineCreateRequest)
        {
            var line = _lineService.CreateLine(lineCreateRequest);
            return CreatedAtAction(nameof(GetLineById), new { id = line.Id }, line);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Client")]
        public IActionResult UpdateLine(int id, [FromBody] LineUpdateRequest lineUpdateRequest)
        {
            try
            {
                _lineService.UpdateLine(id, lineUpdateRequest);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Client")]
        public IActionResult DeleteLine(int id)
        {
            try
            {
                _lineService.DeleteLine(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
