using Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISaleService
    {
        IEnumerable<Sale> GetAllSales();
        Sale GetSaleById(int id);
        Sale CreateSale(SaleDto saleDto);
        void UpdateSale(int id, SaleDto saleDto);
        void DeleteSale(int id);
    }
}
