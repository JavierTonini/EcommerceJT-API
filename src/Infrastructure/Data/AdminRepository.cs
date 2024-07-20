using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AdminRepository : UserRepository<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationContext context) : base(context)
        {
        }

        // Métodos específicos para Admin si es necesario
    }
}
