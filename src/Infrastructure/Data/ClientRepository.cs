using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ClientRepository : UserRepository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationContext context) : base(context)
        {
        }

        // Métodos específicos para Client si es necesario
    }
}
