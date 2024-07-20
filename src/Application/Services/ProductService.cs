using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll().Where(p => p.ProductState).ToList();
        }

        public List<Product> GetAllIncludingInactive()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public Product Create(ProductCreateRequest productCreateRequest)
        {
            var product = new Product
            {
                Name = productCreateRequest.Name,
                Price = productCreateRequest.Price,
                Stock = productCreateRequest.Stock,
                ProductState = productCreateRequest.ProductState
            };
            return _productRepository.Add(product);
        }

        public void Update(int id, ProductUpdateRequest productUpdateRequest)
        {
            var obj = _productRepository.GetById(id);
            obj.Price = productUpdateRequest.Price;
            obj.Stock = productUpdateRequest.Stock;
            obj.ProductState = productUpdateRequest.ProductState;
            _productRepository.Update(obj);
        }

        public void Delete(int id)
        {
            var objToDelete = _productRepository.GetById(id);
            _productRepository.Delete(objToDelete);
        }
    }
}
