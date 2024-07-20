using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _clientRepository.GetAll();
        }

        public Client GetClientById(int id)
        {
            var client = _clientRepository.GetById(id);
            if (client == null)
            {
                throw new NotFoundException($"Client with id {id} not found.");
            }
            return client;
        }

        public Client CreateClient(ClientCreateRequest clientCreateRequest)
        {
            var client = new Client
            {
                Name = clientCreateRequest.Name,
                LastName = clientCreateRequest.LastName,
                UserName = clientCreateRequest.UserName,
                Email = clientCreateRequest.Email,
                Password = clientCreateRequest.Password,
                PhoneNumber = clientCreateRequest.PhoneNumber,
                UserType = "Client"
            };
            return _clientRepository.Add(client);
        }

        public void UpdateClient(int id, ClientUpdateRequest clientUpdateRequest)
        {
            var client = _clientRepository.GetById(id);
            if (client == null)
            {
                throw new NotFoundException($"Client with id {id} not found.");
            }
            client.UserName = clientUpdateRequest.UserName ?? client.UserName;
            client.Email = clientUpdateRequest.Email ?? client.Email;
            client.Password = clientUpdateRequest.Password ?? client.Password;
            client.PhoneNumber = clientUpdateRequest.PhoneNumber ?? client.PhoneNumber;
            _clientRepository.Update(client);
        }

        public void DeleteClient(int id)
        {
            var client = _clientRepository.GetById(id);
            if (client == null)
            {
                throw new NotFoundException($"Client with id {id} not found.");
            }
            _clientRepository.Delete(id);
        }
    }
}
