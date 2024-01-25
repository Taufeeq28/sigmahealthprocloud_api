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
using System.Security.Cryptography;
using NetTopologySuite.Densify;
using System.Security.Principal;

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
        public async Task<ApiResponse<string>> CreateEntityAddress(CreateEntityAddressRequest obj)
        {
            try
            {
           

                    using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                    {
                        try
                        {
                           
                            var newCreateEntityAddress = new  EntityAddress
                            {
                                EntityType = obj.EntityType,
                                AddressType = obj.AddressType,
                                Addressid = obj.Addressid,
                                CreatedDate = DateTime.UtcNow,
                                UpdatedDate = DateTime.UtcNow,
                                CreatedBy = obj.CreatedBy,
                                Isprimary = false,
                                EntityId = obj.EntityId
                            };

                            _dbContext.EntityAddresses.Add(newCreateEntityAddress);

                            await _dbContext.SaveChangesAsync();

                            transaction.Commit();

                            return ApiResponse<string>.Success(null, $"Entity address inserted successfully.");
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                
                
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_correlationId} - Exception occurred in Method: {nameof(CreateEntityAddress)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while creating entity address.");
            }
        }
        public async Task<ApiResponse<GetAddressesResponse>> GetEntityAddresses(GetEntityAddressesRequest getAddressesRequest)
        {
            try
            {
                var normalizedIdentifier = getAddressesRequest.Identifier?.ToLower() ?? string.Empty;
                var formattedIdentifier = $"%{normalizedIdentifier}%";

                var query = from entityAddress in _dbContext.Set<EntityAddress>()
                            join address in _dbContext.Set<Address>() on entityAddress.Addressid equals address.Id
                            join country in _dbContext.Set<Country>() on address.CountryId equals country.Id
                            where (entityAddress.EntityId == getAddressesRequest.EntityId &&
                                   (string.IsNullOrWhiteSpace(getAddressesRequest.Identifier) ||
                                    EF.Functions.Like(address.Line1.ToLower(), formattedIdentifier) ||
                                    EF.Functions.Like(address.Line2.ToLower(), formattedIdentifier) ||
                                    EF.Functions.Like(address.Suite.ToLower(), formattedIdentifier) ||
                                    EF.Functions.Like(address.Country.CountryName.ToLower(), formattedIdentifier) ||
                                    EF.Functions.Like(address.County.CountyName.ToLower(), formattedIdentifier) ||
                                    EF.Functions.Like(address.State.StateName.ToLower(), formattedIdentifier) ||
                                    EF.Functions.Like(address.City.CityName.ToLower(), formattedIdentifier) ||
                                    EF.Functions.Like(address.ZipCode.ToLower(), formattedIdentifier)))
                            select new GetAddressesResponse
                            {
                                Id = entityAddress.Id,
                                AddressId = address.AddressId,
                                Line1 = address.Line1,
                                Line2 = address.Line2,
                                Suite = address.Suite,
                                CountyName = address.County.CountyName,
                                CountryName = country.CountryName,
                                StateName = address.State.StateName,
                                CityName = address.City.CityName,
                                ZipCode = address.ZipCode
                            };

                var resultQuery = query.OrderBy(a => a.ZipCode);

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

        public async Task<ApiResponse<string>> UpdateEntityAddress(UpdateEntityAddressRequest obj)
        {
            try
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var existingEntityAddress = await _dbContext.EntityAddresses.FindAsync(obj.id);

                        if (existingEntityAddress == null)
                        {
                            transaction.Rollback();
                            return ApiResponse<string>.Fail("Entity address not found.");
                        }


                        existingEntityAddress.EntityType = obj.EntityType ?? existingEntityAddress.EntityType;
                        existingEntityAddress.AddressType = obj.AddressType ?? existingEntityAddress.AddressType;
                        existingEntityAddress.Addressid = obj.Addressid ?? existingEntityAddress.Addressid;
                        existingEntityAddress.UpdatedDate = DateTime.UtcNow;
                        existingEntityAddress.UpdatedBy = obj.UpdatedBy ?? existingEntityAddress.UpdatedBy;
                        existingEntityAddress.Isprimary = obj.Isprimary ?? existingEntityAddress.Isprimary;

                        await _dbContext.SaveChangesAsync();

                        transaction.Commit();
                        return ApiResponse<string>.Success(null, "Entity address updated successfully.");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_correlationId} - Exception occurred in Method: {nameof(UpdateEntityAddress)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while updating entity address.");
            }
        }
        public async Task<ApiResponse<string>> DeleteEntityAddress(Guid entityAddressId)
        {
            try
            {
               // using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                //{
                    try
                    {
                        var entityAddress = await _dbContext.EntityAddresses.FindAsync(entityAddressId);

                        if (entityAddress == null)
                        {
                            //transaction.Rollback();
                            return ApiResponse<string>.Fail("Entity address not found.");
                        }

                        _dbContext.EntityAddresses.Remove(entityAddress);
                        await _dbContext.SaveChangesAsync();

                        //transaction.Commit();

                        return ApiResponse<string>.Success(null, "Entity address deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        //transaction.Rollback();
                        _logger.LogError($"CorrelationId: {_correlationId} - Exception occurred while deleting entity address: {ex.Message}, Stack trace: {ex.StackTrace}");
                        return ApiResponse<string>.Fail($"An error occurred while deleting entity address: {ex.Message}");
                    }
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Exception occurred while deleting entity address: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<string>.Fail($"An error occurred while deleting entity address: {ex.Message}");
            }
        }
        public async Task<ApiResponse<string>> CreateMasterAddress(CreateMasterAddressRequest obj)
        {
            try
            {
                var generateNextIdRequest = new GenerateNextIdRequest
                {
                    output_table_name = Constants.OUTPUT_TABLE_NAME_ADDRESSES,
                    start_column_name = Constants.START_CLOUMN_NAME_ADDRESS_ID_START,
                    suffix_column_name = Constants.SUFFIX_CLOUMN_NAME_ADDRESS_ID_SUFFIX,
                    output_column_name = Constants.OUTPUT_CLOUMN_NAME_ADDRESS_ID
                };

                MasterDataService _masterdataservice = new MasterDataService(_dbContext, Constants.CreateLogger<MasterDataService>(), _dbContextudf);

                var nextIdApiResponse = await _masterdataservice.GenerateNextId(generateNextIdRequest);

                if (nextIdApiResponse.Status == ApiResponsesConstants.SUCCESS_STATUS)
                {
                    string nextId = nextIdApiResponse.Data.next_id.ToString();


                    using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            List<Data.Models.Country> countryEntities = _dbContext.Countries.ToList();

                            List<BAL.Request.CountrySearch> countrySearchEntities = countryEntities
                                .Select(c => new BAL.Request.CountrySearch
                                {
                                    Id = c.Id,
                                    CountryName = c.CountryName
                                })
                                .ToList();

                            string targetCountryName = obj.CountryName;
                            Guid countryId = FindIdByFuzzyMatch(countrySearchEntities, targetCountryName);
                            Console.WriteLine($"CountryId: {countryId}");

                            string targetStateName = obj.StateName;
                            List<Data.Models.State> stateEntities = _dbContext.States.Where(c => c.StateName.Contains(targetStateName) || c.StateName.StartsWith(targetStateName.Substring(0, 1)))
      .ToList();
                            List<BAL.Request.StateSearch> stateSearchEntities = stateEntities
                                .Select(s => new BAL.Request.StateSearch
                                {
                                    Id = s.Id,
                                    StateName = s.StateName
                                })
                                .ToList();

                            
                            Guid stateId = FindIdByFuzzyMatch(stateSearchEntities, targetStateName);
                            Console.WriteLine($"StateId: {stateId}");


                            // Example usage for CountySearch
                            string targetCountyName = obj.CountyName;
                            List<Data.Models.County> countyEntities = _dbContext.Counties.Where(c => c.CountyName.Contains(targetCountyName) || c.CountyName.StartsWith(targetCountyName.Substring(0, 1)))
      .ToList();
                            List<BAL.Request.CountySearch> countySearchEntities = countyEntities
                                .Select(c => new BAL.Request.CountySearch
                                {
                                    Id = c.Id,
                                    CountyName = c.CountyName
                                })
                                .ToList();

                           
                            Guid countyId = FindIdByFuzzyMatch(countySearchEntities, targetCountyName);
                            Console.WriteLine($"CountyId: {countyId}");


                            // Example usage for CitySearch
                            string targetCityName = obj.CityName;
                            List<Data.Models.City> cityEntities = _dbContext.Cities
      .Where(c => c.CityName.Contains(targetCityName) || c.CityName.StartsWith(targetCityName.Substring(0, 1)))
      .ToList();
                            List<BAL.Request.CitySearch> citySearchEntities = cityEntities
                                .Select(c => new BAL.Request.CitySearch
                                {
                                    Id = c.Id,
                                    CityName = c.CityName
                                })
                                .ToList();

                          
                            Guid cityId = FindIdByFuzzyMatch(citySearchEntities, targetCityName);
                            Console.WriteLine($"CityId: {cityId}");


                            var newCreateAddress = new Address
                            {
                                AddressId = nextId,
                                Line1 = obj.Line1,
                                Line2 = obj.Line2,
                                Suite = obj.Suite,
                                CountryId = countryId,
                                StateId = stateId,
                                CountyId = countyId,
                                CityId = cityId,
                                ZipCode = obj.ZipCode,
                                CreatedDate = DateTime.UtcNow,
                                UpdatedDate = DateTime.UtcNow,
                                CreatedBy = obj.CreatedBy
                            };

                            _dbContext.Addresses.Add(newCreateAddress);

                            await _dbContext.SaveChangesAsync();

                            transaction.Commit();

                            return ApiResponse<string>.Success(null, $"Address inserted successfully.");
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
                else
                {
                    return ApiResponse<string>.Fail($"Failed to generate address next ID: {nextIdApiResponse.Message}");
                }

            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_correlationId} - Exception occurred in Method: {nameof(CreateMasterAddress)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while creating Master address.");
            }
        }
        #region Private Methods
        static Guid FindIdByFuzzyMatch<T>(List<T> entities, string targetName) where T : IEntity
        {
            string bestMatchName = entities
                .OrderBy(e => ComputeLevenshteinDistance(targetName, e.GetName()))
                .Select(e => e.GetName())
                .FirstOrDefault();

            if (bestMatchName != null)
            {
                return entities.First(e => e.GetName() == bestMatchName).GetId();
            }

            // Return a default ID when no match is found
            return DefaultIdForType<T>();
        }

        static Guid DefaultIdForType<T>() where T : IEntity
        {
            // You can return a default ID based on your requirements
            if (typeof(T) == typeof(CountrySearch))
            {
                return new Guid("d885c47c-dc01-4fbc-8aa1-12b7b4d500b6");
            }
            else if (typeof(T) == typeof(StateSearch))
            {
                return new Guid("ef11ae6c-b76b-4810-bcf9-c8bee37ff1de");
            }
            else if (typeof(T) == typeof(CountySearch))
            {
                return new Guid("058fbb4b-279b-4001-9ee4-b439e5cd4ec6");
            }
            else if (typeof(T) == typeof(CitySearch))
            {
                return new Guid("65e1255c-8d4c-4d31-875d-36c8e92b5b41");
            }

            return Guid.Empty;
        }

        static int ComputeLevenshteinDistance(string s, string t)
        {
            int[,] distance = new int[s.Length + 1, t.Length + 1];

            for (int i = 0; i <= s.Length; i++)
            {
                for (int j = 0; j <= t.Length; j++)
                {
                    if (i == 0)
                    {
                        distance[i, j] = j;
                    }
                    else if (j == 0)
                    {
                        distance[i, j] = i;
                    }
                    else
                    {
                        distance[i, j] = Math.Min(Math.Min(
                            distance[i - 1, j] + 1,
                            distance[i, j - 1] + 1),
                            distance[i - 1, j - 1] + (s[i - 1] == t[j - 1] ? 0 : 1));
                    }
                }
            }

            return distance[s.Length, t.Length];
        }

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
