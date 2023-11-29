using Data;
using Data.Models;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class UsersRepository : IUsersRepository,IGenericRepository<User>
    {
        
        private SigmaproIisContext context;

        public UsersRepository(SigmaproIisContext _context)
        {
            this.context = _context;
        }
      
        public void Add(User entity)
        {
            context.Users.Add(entity);
        }

        public void AddRange(IEnumerable<User> entities)
        {
            context.Users.AddRange(entities);
        }

        public User Authenticate(User users)
        {
            var usermodel = context.Users.FirstOrDefault(u=>u.UserId==users.UserId);
            if (usermodel != null && usermodel.UserId.Equals(users.UserId) && usermodel.Password.Equals(users.Password))
            {
                return usermodel;
            }
            return null;
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User? GetById(int id)
        {
            return (User?)context.Users.Find(id);
        }

        public void Remove(User entity)
        {
           context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<User> entities)
        {
            context.RemoveRange(entities);
        }
    }
}
