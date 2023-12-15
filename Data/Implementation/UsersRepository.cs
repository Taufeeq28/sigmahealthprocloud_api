using Data;
using Data.Models;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private ILogger<UnitOfWork> _logger;

        public UsersRepository(SigmaproIisContext _context,ILogger<UnitOfWork> logger)
        {
            this.context = _context;
            _logger = logger;
        }
      
        public void Add(User entity)
        {
            try
            {
                context.Users.Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(Add)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public void AddRange(IEnumerable<User> entities)
        {
            try
            {
                context.Users.AddRange(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(AddRange)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public User Authenticate(User users)
        {
            try
            {
                var usermodel = context.Users.Where(u => u.UserId == users.UserId).FirstOrDefault();
                if (usermodel != null && usermodel.UserId.Equals(users.UserId) && usermodel.Password.Equals(users.Password))
                {
                    return usermodel;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(Authenticate)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            try
            {
                return context.Users.Where(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(Find)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                return context.Users.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(GetAll)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public User? GetById(int id)
        {
            try
            {
                return (User?)context.Users.Find(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(GetById)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public void Remove(User entity)
        {
            try
            {
                context.Remove(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(Remove)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public void RemoveRange(IEnumerable<User> entities)
        {
            try
            {
                context.RemoveRange(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(RemoveRange)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }
    }
}
