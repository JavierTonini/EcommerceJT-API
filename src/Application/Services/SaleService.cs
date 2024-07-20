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
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public IEnumerable<Sale> GetAllSales()
        {
            return _saleRepository.GetAll();
        }

        public Sale GetSaleById(int id)
        {
            var sale = _saleRepository.GetById(id);
            if (sale == null)
            {
                throw new NotFoundException($"Sale with id {id} not found.");
            }
            return sale;
        }

        public Sale CreateSale(SaleDto saleDto)
        {
            var sale = new Sale
            {
                ClientId = saleDto.ClientId
            };

            return _saleRepository.Add(sale);
        }

        public void UpdateSale(int id, SaleDto saleDto)
        {
            var sale = _saleRepository.GetById(id);
            if (sale == null)
            {
                throw new NotFoundException($"Sale with id {id} not found.");
            }

            sale.ClientId = saleDto.ClientId;

            _saleRepository.Update(sale);
        }
        public void DeleteSale(int id)
        {
            var sale = _saleRepository.GetById(id);
            if (sale == null)
            {
                throw new NotFoundException($"Sale with id {id} not found.");
            }
            _saleRepository.Delete(id);
        }
    }
}
