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
    public class LineService : ILineService
    {
        private readonly ILineRepository _lineRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISaleRepository _saleRepository;

        public LineService(ILineRepository lineRepository, IProductRepository productRepository, ISaleRepository saleRepository)
        {
            _lineRepository = lineRepository;
            _productRepository = productRepository;
            _saleRepository = saleRepository;
        }

        public IEnumerable<LineGetRequest> GetAllLines()
        {
            var lines = _lineRepository.GetAll();
            return lines.Select(line => new LineGetRequest
            {
                Id = line.Id,
                SaleId = line.SaleId,
                ProductId = line.ProductId,
                Quantity = line.Quantity,
                UnitPrice = line.UnitPrice,
            }).ToList();
        }

        public LineGetRequest GetLineById(int id)
        {
            var line = _lineRepository.GetById(id);
            if (line == null)
            {
                throw new NotFoundException($"Line with id {id} not found.");
            }
            return new LineGetRequest
            {
                Id = line.Id,
                SaleId = line.SaleId,
                ProductId = line.ProductId,
                Quantity = line.Quantity,
                UnitPrice = line.UnitPrice,
            };
        }

        public Line CreateLine(LineCreateRequest lineCreateRequest)
        {
            var product = _productRepository.GetById(lineCreateRequest.ProductId);
            if (product == null)
            {
                throw new NotFoundException($"Product with id {lineCreateRequest.ProductId} not found.");
            }

            var line = new Line
            {
                SaleId = lineCreateRequest.SaleId,
                ProductId = lineCreateRequest.ProductId,
                Quantity = lineCreateRequest.Quantity,
                UnitPrice = product.Price
            };

            var createdLine = _lineRepository.Add(line);

            UpdateSaleTotalAmount(line.SaleId);

            return createdLine;
        }

        public void UpdateLine(int id, LineUpdateRequest lineUpdateRequest)
        {
            var line = _lineRepository.GetById(id);
            if (line == null)
            {
                throw new NotFoundException($"Line with id {id} not found.");
            }

            var product = _productRepository.GetById(lineUpdateRequest.ProductId);
            if (product == null)
            {
                throw new NotFoundException($"Product with id {lineUpdateRequest.ProductId} not found.");
            }

            line.ProductId = lineUpdateRequest.ProductId;
            line.Quantity = lineUpdateRequest.Quantity;
            line.UnitPrice = product.Price;

            _lineRepository.Update(line);

            UpdateSaleTotalAmount(line.SaleId);
        }

        public void DeleteLine(int id)
        {
            var line = _lineRepository.GetById(id);
            if (line == null)
            {
                throw new NotFoundException($"Line with id {id} not found.");
            }

            var saleId = line.SaleId;
            _lineRepository.Delete(id);

            UpdateSaleTotalAmount(saleId);
        }

        private void UpdateSaleTotalAmount(int saleId)
        {
            var sale = _saleRepository.GetById(saleId);
            if (sale != null)
            {
                sale.TotalAmount = sale.Lines?.Sum(line => line.TotalPrice) ?? 0;
                _saleRepository.Update(sale);
            }
        }
    }
}
