using Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClientService
    {
        IEnumerable<Client> GetAllClients();
        Client GetClientById(int id);
        Client CreateClient(ClientCreateRequest clientCreateRequest);
        void UpdateClient(int id, ClientUpdateRequest clientUpdateRequest);
        void DeleteClient(int id);
    }
}
