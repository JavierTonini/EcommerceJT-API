using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISaleRepository
    {
        IEnumerable<Sale> GetAll();
        Sale GetById(int id);
        Sale Add(Sale sale);
        void Update(Sale sale);
        void Delete(int id);
    }
}
