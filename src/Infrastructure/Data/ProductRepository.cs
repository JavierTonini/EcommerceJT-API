using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                throw new NotFoundException($"Product with id {id} not found.");
            }
            return product;
        }

        public Product Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public void Update(Product product)
        {
            if (_context.Products.Any(p => p.Id == product.Id))
            {
                _context.Products.Update(product);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException($"Product with id {product.Id} not found.");
            }
        }

        public void Delete(Product product)
        {
            if (_context.Products.Any(p => p.Id == product.Id))
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException($"Product with id {product.Id} not found.");
            }
        }
    }
}
