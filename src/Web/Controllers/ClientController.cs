using Application.Interfaces;
using Application.Models.Request;
using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ClientCreateRequest clientCreateRequest)
        {
            var newClient = _clientService.CreateClient(clientCreateRequest);
            return CreatedAtAction(nameof(Get), new { id = newClient.Id }, newClient);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Client,Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] ClientUpdateRequest clientUpdateRequest)
        {
            try
            {
                _clientService.UpdateClient(id, clientUpdateRequest);
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
                _clientService.DeleteClient(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<Client>> GetAll()
        {
            return Ok(_clientService.GetAllClients());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Client> Get([FromRoute] int id)
        {
            try
            {
                return Ok(_clientService.GetClientById(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
