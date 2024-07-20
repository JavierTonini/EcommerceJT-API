using Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAll();

        List<Product> GetAllIncludingInactive();

        Product GetById(int id);

        Product Create(ProductCreateRequest productCreateRequest);

        void Update(int id, ProductUpdateRequest productUpdateRequest);

        void Delete(int id);
    }
}
