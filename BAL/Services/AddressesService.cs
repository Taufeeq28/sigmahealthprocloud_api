using BAL.Interfaces;
using BAL.Responses;
using Data.Models;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using BAL.Constant;
using System.Data.Common;
using BAL.Request;
using System.Linq;

namespace BAL.Services
{
    public class AddressesService : DataAccessProvider<Address>, IAddressesService
    {
        private readonly SigmaproIisContext _dbContext;
        private readonly SigmaproIisContextUdf _dbContextudf;
        private readonly ILogger<AddressesService> _logger;
        private readonly string _correlationId = string.Empty;

        public AddressesService(SigmaproIisContext dbContext, ILogger<AddressesService> logger, SigmaproIisContextUdf dbContextudf) : base(dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
            _dbContextudf = dbContextudf;
        }
        public async Task<ApiResponse<GetAddressesResponse>> GetAddresses(GetAddressesRequest getAddressesRequest)
        {
            try
            {
                var query = _dbContext.Set<Address>().AsQueryable();

                Expression<Func<Address, bool>> whereCondition = null;

                query = query.Include(a => a.County)
                              .Include(a => a.Country)
                              .Include(a => a.State)
                              .Include(a => a.City);

                if (!string.IsNullOrWhiteSpace(getAddressesRequest.identifier))
                {
                    whereCondition = BuildWhereCondition(getAddressesRequest.identifier);
                    query = query.Where(whereCondition);
                }

                var resultQuery = query.Select(a => new GetAddressesResponse
                {
                    Id = a.Id,
                    AddressId = a.AddressId,
                    Line1 = a.Line1,
                    Line2 = a.Line2,
                    Suite = a.Suite,
                    CountyName = a.County.CountyName,
                    CountryName = a.Country.CountryName,
                    StateName = a.State.StateName,
                    CityName = a.City.CityName,
                    ZipCode = a.ZipCode
                })
                .OrderBy(a => a.ZipCode);

               
                var result = await resultQuery.ToListAsync();

               
                result.ForEach(a =>
                {
                    a.FullAddress = $"{a.Line1} {a.Line2} {a.Suite} {a.CountyName} {a.CountryName} {a.StateName} {a.CityName} {a.ZipCode}";
                });

                
                result = result.Take(getAddressesRequest.RecordCount ?? 500).ToList();

                return ApiResponse<GetAddressesResponse>.SuccessList(result, "Addresses fetched successfully!");
            }
            catch (DbException ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Database exception: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<GetAddressesResponse>.Fail($"A database error occurred while fetching addresses: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Exception occurred in GetAddresses: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<GetAddressesResponse>.Fail($"An error occurred while fetching addresses: {ex.Message}");
            }
        }


        #region Private Methods
        private Expression<Func<Address, bool>> BuildWhereCondition(string identifier)
        {
            Expression<Func<Address, bool>> whereCondition = null;

            var normalizedIdentifier = identifier.ToLower();
            var formattedIdentifier = $"%{normalizedIdentifier}%";

            whereCondition = CombineConditions(whereCondition, a => EF.Functions.Like(a.Line1.ToLower(), formattedIdentifier));
            whereCondition = CombineConditions(whereCondition, a => EF.Functions.Like(a.Line2.ToLower(), formattedIdentifier));
            whereCondition = CombineConditions(whereCondition, a => EF.Functions.Like(a.Suite.ToLower(), formattedIdentifier));
            whereCondition = CombineConditions(whereCondition, a => EF.Functions.Like(a.Country.CountryName.ToLower(), formattedIdentifier));
            whereCondition = CombineConditions(whereCondition, a => EF.Functions.Like(a.County.CountyName.ToLower(), formattedIdentifier));
            whereCondition = CombineConditions(whereCondition, a => EF.Functions.Like(a.State.StateName.ToLower(), formattedIdentifier));
            whereCondition = CombineConditions(whereCondition, a => EF.Functions.Like(a.City.CityName.ToLower(), formattedIdentifier));
            whereCondition = CombineConditions(whereCondition, a => EF.Functions.Like(a.ZipCode.ToLower(), formattedIdentifier));
           

            return whereCondition;
        }

        private Expression<Func<Address, bool>> CombineConditions(Expression<Func<Address, bool>> existingCondition, Expression<Func<Address, bool>> newCondition)
        {
            if (existingCondition == null)
                return newCondition;

            var parameter = Expression.Parameter(typeof(Address));
            var body = Expression.OrElse(
                Expression.Invoke(existingCondition, parameter),
                Expression.Invoke(newCondition, parameter)
            );

            return Expression.Lambda<Func<Address, bool>>(body, parameter);
        }
        #endregion
    }

}
