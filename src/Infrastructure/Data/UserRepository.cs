using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UserRepository<T> : IUserRepository<T> where T : User
    {
        protected readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Users.OfType<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Users.OfType<T>().FirstOrDefault(u => u.Id == id);
        }

        public T GetByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName) as T;
        }

        public T Add(T entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Update(T entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Users.OfType<T>().FirstOrDefault(u => u.Id == id);
            if (entity != null)
            {
                _context.Users.Remove(entity);
                _context.SaveChanges();
            }
        }

    }
}
