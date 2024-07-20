using Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        IEnumerable<AdminGetRequest> GetAllAdmins();
        AdminGetRequest GetAdminById(int id);
        Admin CreateAdmin(AdminCreateRequest AdminCreateRequest);
        void UpdateAdmin(int id, AdminUpdateRequest adminUpdateRequest);
        void DeleteAdmin(int id);
    }
}
