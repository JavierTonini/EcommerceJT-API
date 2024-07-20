using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class ClientCreateRequest
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
