using BAL.Constant;
using BAL.Interfaces;
using BAL.Request;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;


namespace BAL.Services
{
    
    public class FacilityService : DataAccessProvider<Facility>, IFacilityService
    {
        private readonly SigmaproIisContext _dbContext;
       // private readonly ILogger<FacilityService> _logger;
        private readonly string _corelationId=string.Empty;
        public FacilityService(SigmaproIisContext dbContext) :base(dbContext)
        {
        _dbContext = dbContext;
        //_logger = logger;
        }
        #region Public Methods
        public async Task<PaginationModel<FacilitySearchResponse>> FacilitySearch(FacilitySearchRequest request)
        {
            try
            {

                var (sql, parameters) = FormQueryAndParamsForFetchingFacilitySearch(request);
                var result = await _dbContext.FacilitySearch.FromSqlRaw(sql, parameters.ToArray()).ToListAsync();
             
                return PaginationHelper.Paginate(result, request.pageNumber, request.pageSize);
            }
            catch (Exception exp)
            {
               // _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(FacilitySearch)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                throw new Exception(exp?.Message);
            }
        }

        #endregion

        #region Private Methods
        private (string Query, List<NpgsqlParameter> FunctionParams) FormQueryAndParamsForFetchingFacilitySearch(FacilitySearchRequest request)
        {
            var trimmedIdentifiers = new string[0];

        trimmedIdentifiers= request.identifiers.Select(id => id.ToString().Trim()).ToArray();

            List<NpgsqlParameter> functionParams = new()
    {
        new NpgsqlParameter { ParameterName = "@identifier", Value = trimmedIdentifiers == null ? DBNull.Value : (object)trimmedIdentifiers },
        new NpgsqlParameter { ParameterName = "@pagenumber", Value = request.pageNumber == 0 ? DBNull.Value : (object)request.pageNumber },
        new NpgsqlParameter { ParameterName = "@pagesize", Value = request.pageSize == 0 ? DBNull.Value : (object)request.pageSize },
        new NpgsqlParameter { ParameterName = "@sortby", Value = request.sortBy == null ? DBNull.Value : (object)request.sortBy},
        new NpgsqlParameter { ParameterName = "@sortdirection", Value = request.sortDirection == null ? DBNull.Value : (object)request.sortDirection },
        new NpgsqlParameter { ParameterName = "@searchfacilityname", Value = request.searchFacilityName == null ? DBNull.Value : (object)request.searchFacilityName },
        new NpgsqlParameter { ParameterName = "@searchjurisdiction", Value = request.searchJurisdiction== null ? DBNull.Value : (object)request.searchJurisdiction },
        new NpgsqlParameter { ParameterName = "@searchorganization", Value = request.searchOrganization == null ? DBNull.Value : (object)request.searchOrganization },
        new NpgsqlParameter { ParameterName = "@searchaddress", Value = request.searchAddress == null ? DBNull.Value : (object)request.searchAddress },
        new NpgsqlParameter { ParameterName = "@searchcity", Value = request.searchCity == null ? DBNull.Value : (object)request.searchCity },
        new NpgsqlParameter { ParameterName = "@searchstate", Value = request.searchState == null ? DBNull.Value : (object)request.searchState },
        new NpgsqlParameter { ParameterName = "@searchzipcode", Value = request.searchZipCode == null ? DBNull.Value : (object)request.searchZipCode },
    };

            string sql = "SELECT * FROM public.udf_searchfacilityresult(@identifier, @pagenumber, @pagesize, @sortby, @sortdirection, @searchfacilityname, @searchjurisdiction, @searchorganization, @searchaddress, @searchcity, @searchstate, @searchzipcode)";

            return (sql, functionParams);
        }

        #endregion

    }
}
