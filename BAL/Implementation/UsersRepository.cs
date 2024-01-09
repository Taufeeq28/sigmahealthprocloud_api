using BAL.Repository;
using BAL.Constant;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BAL.RequestModels;

namespace BAL.Implementation
{
    public class UsersRepository : IUsersRepository, IGenericRepository<User>
    {

        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;

        public UsersRepository(SigmaproIisContext _context, ILogger<UnitOfWork> logger)
        {
            context = _context;
            _logger = logger;
        }


        public Userloginmodel Authenticate(User users)
        {
            try
            {
                var usermodel = context.Users.Where(u => u.UserId == users.UserId).FirstOrDefault();
                var usermod = context.Users.Join(context.Facilities, u => u.Id, f => f.UserId, (u, f) => new { users = u, fid = f.Id, orgid = f.OrganizationsId, facilities = f }).
                    Join(context.Organizations, f => f.orgid, o => o.Id, (f, o) => new { facility = f.facilities, f.users, org = o, juridictionid = o.JuridictionId }).
                    Join(context.Juridictions, o => o.juridictionid, j => j.Id, (o, j) => new { facility = o.facility, o.users, o.org, jurd = j })
                    .Where(u => u.users.UserId == users.UserId).
                    Select(i=>new
                    {
                        i.users.UserId,
                        i.users.Password,
                        i.users.UserType,
                        i.facility.FacilityName,
                        i.jurd.JuridictionName
                    }
                    ).FirstOrDefault();
                var model = new Userloginmodel()
                {
                    UserId = usermod.UserId,
                    Password = usermod.Password,
                    UserRole = usermod.UserType,
                    FacilityName = usermod.FacilityName,
                    JuridictionName = usermod.JuridictionName
                };
                    
                if (usermod != null && usermod.UserId.Equals(users.UserId) && usermod.Password.Equals(users.Password))
                {
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(Authenticate)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public async Task<IEnumerable<User>> Find(Expression<Func<User, bool>> predicate)
        {
            return await context.Users.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await context.Set<User>().ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await context.Set<User>().FindAsync(id);
        }

        public async Task<ApiResponse<string>> InsertAsync(User entity)
        {
            try
            {
                await context.Set<User>().AddAsync(entity);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "User inserted successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Inserting the User.");
            }
        }

        public async Task<ApiResponse<string>> UpdateAsync(User entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "User Updated successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Updating the User.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await context.Set<User>().FindAsync(id);
                if (entity != null)
                {
                    context.Set<User>().Remove(entity);
                    await context.SaveChangesAsync();
                    return ApiResponse<string>.Success(id.ToString(), "User deleted successfully.");
                }

                return ApiResponse<string>.Fail("User with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("User with the given ID not found.");
            }
        }
    }
}
