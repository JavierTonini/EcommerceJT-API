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
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AdminGetRequest>> GetAll()
        {
            return Ok(_adminService.GetAllAdmins());
        }

        [HttpGet("{id}")]
        public ActionResult<AdminGetRequest> Get(int id)
        {
            try
            {
                return Ok(_adminService.GetAdminById(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] AdminCreateRequest adminCreateRequest)
        {
            var newAdmin = _adminService.CreateAdmin(adminCreateRequest);
            return CreatedAtAction(nameof(Get), new { id = newAdmin.Id }, newAdmin);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AdminUpdateRequest adminUpdateRequest)
        {
            try
            {
                _adminService.UpdateAdmin(id, adminUpdateRequest);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _adminService.DeleteAdmin(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}