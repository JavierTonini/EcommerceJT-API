using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationContext _context;

        public SaleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Sale> GetAll()
        {
            return _context.Sales
                .Include(s => s.Lines)
                .ThenInclude(l => l.Product)
                .ToList();
        }

        public Sale GetById(int id)
        {
            return _context.Sales
                .Include(s => s.Lines)
                .ThenInclude(l => l.Product)
                .FirstOrDefault(s => s.Id == id);
        }

        public Sale Add(Sale sale)
        {
            _context.Sales.Add(sale);
            _context.SaveChanges();
            return sale;
        }

        public void Update(Sale sale)
        {
            _context.Sales.Update(sale);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var sale = _context.Sales.Find(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                _context.SaveChanges();
            }
        }
    }
}
