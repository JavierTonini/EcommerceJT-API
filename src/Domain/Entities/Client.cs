using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client : User
    {
        public string PhoneNumber { get; set; }
        public ICollection<Sale> Sales { get; set; }

    }
}
