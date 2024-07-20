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
    public class LineRepository : ILineRepository
    {
        private readonly ApplicationContext _context;

        public LineRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Line> GetAll()
        {
            return _context.Lines.ToList();
        }

        public Line GetById(int id)
        {
            return _context.Lines.FirstOrDefault(l => l.Id == id);
        }

        public Line Add(Line line)
        {
            _context.Lines.Add(line);
            _context.SaveChanges();
            return line;
        }

        public void Update(Line line)
        {
            _context.Lines.Update(line);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var line = _context.Lines.Find(id);
            if (line != null)
            {
                _context.Lines.Remove(line);
                _context.SaveChanges();
            }
        }
    }
}
