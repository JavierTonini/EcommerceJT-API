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
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public IEnumerable<AdminGetRequest> GetAllAdmins()
        {
            return _adminRepository.GetAll().Select(admin => new AdminGetRequest
            {
                Id = admin.Id,
                Name = admin.Name,
                LastName = admin.LastName,
                UserName = admin.UserName,
                Email = admin.Email
            }).ToList();
        }

        public AdminGetRequest GetAdminById(int id)
        {
            var admin = _adminRepository.GetById(id);
            if (admin == null)
            {
                throw new NotFoundException($"Admin with id {id} not found.");
            }

            return new AdminGetRequest
            {
                Id = admin.Id,
                Name = admin.Name,
                LastName = admin.LastName,
                UserName = admin.UserName,
                Email = admin.Email
            };
        }

        public Admin CreateAdmin(AdminCreateRequest adminCreateRequest)
        {
            var admin = new Admin
            {
                Name = adminCreateRequest.Name,
                LastName = adminCreateRequest.LastName,
                UserName = adminCreateRequest.UserName,
                Email = adminCreateRequest.Email,
                Password = adminCreateRequest.Password,
                UserType = "Admin"
            };
            return _adminRepository.Add(admin);
        }

        public void UpdateAdmin(int id, AdminUpdateRequest adminUpdateRequest)
        {
            var admin = _adminRepository.GetById(id);
            if (admin == null)
            {
                throw new NotFoundException($"Admin with id {id} not found.");
            }
            admin.UserName = adminUpdateRequest.UserName ?? admin.UserName;
            admin.Email = adminUpdateRequest.Email ?? admin.Email;
            admin.Password = adminUpdateRequest.Password ?? admin.Password;
            _adminRepository.Update(admin);
        }

        public void DeleteAdmin(int id)
        {
            var admin = _adminRepository.GetById(id);
            if (admin == null)
            {
                throw new NotFoundException($"Admin with id {id} not found.");
            }
            _adminRepository.Delete(id);
        }
    }
}