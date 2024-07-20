using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILineRepository
    {
        IEnumerable<Line> GetAll();
        Line GetById(int id);
        Line Add(Line line);
        void Update(Line line);
        void Delete(int id);
    }
}
