using BAL.Constant;
using BAL.Interfaces;
using BAL.Request;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using NpgsqlTypes;


namespace BAL.Services
{

    public class FacilityService : DataAccessProvider<Facility>, IFacilityService
    {
        private readonly SigmaproIisContext _dbContext;
       private readonly ILogger<FacilityService> _logger;
        private readonly string _corelationId=string.Empty;
        public FacilityService(SigmaproIisContext dbContext, ILogger<FacilityService> logger) :base(dbContext)
        {
        _dbContext = dbContext;
        _logger = logger;
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
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(FacilitySearch)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                throw new Exception(exp?.Message);
            }
        }
        public async Task<ApiResponse<bool>> DeleteFacility(Guid facilityId)
        {
            try
            {
                var facility = await _dbContext.Facilities.FindAsync(facilityId);

                if (facility != null)
                {
                    facility.Isdelete = true; 
                    _dbContext.Facilities.Update(facility);
                    await _dbContext.SaveChangesAsync();

                    return ApiResponse<bool>.Success(true, "Facility deleted successfully."); ; 
                }

                return ApiResponse<bool>.Fail(false,"Facility with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteFacility)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<bool>.Fail(false, "Facility with the given ID not found.");
            }
        }
        #endregion

        #region Private Methods
        private (string Query, List<NpgsqlParameter> FunctionParams) FormQueryAndParamsForFetchingFacilitySearch(FacilitySearchRequest request)
        {
       
            List<NpgsqlParameter> functionParams = new()
{

    new NpgsqlParameter
    {
        ParameterName = "@identifier",
        Value = request.identifier == null ? DBNull.Value : request.identifier.Trim(),
        NpgsqlDbType = NpgsqlDbType.Varchar
    },
    new NpgsqlParameter
    {
        ParameterName = "@pagenumber",
        Value = request.pageNumber == 1 ? DBNull.Value : request.pageNumber,
        NpgsqlDbType = NpgsqlDbType.Integer
    },
    new NpgsqlParameter
    {
        ParameterName = "@pagesize",
        Value = request.pageSize == 10 ? DBNull.Value : request.pageSize,
        NpgsqlDbType = NpgsqlDbType.Integer
    },
    new NpgsqlParameter
    {
        ParameterName = "@sortby",
        Value = request.sortBy == null ? DBNull.Value : request.sortBy,
        NpgsqlDbType = NpgsqlDbType.Varchar
    },
    new NpgsqlParameter
    {
        ParameterName = "@sortdirection",
        Value = request.sortDirection == null ? DBNull.Value : request.sortDirection,
        NpgsqlDbType = NpgsqlDbType.Varchar
    },
    new NpgsqlParameter
    {
        ParameterName = "@searchfacilityname",
        Value = request.searchFacilityName == null ? DBNull.Value : request.searchFacilityName,
        NpgsqlDbType = NpgsqlDbType.Varchar
    },
    new NpgsqlParameter
    {
        ParameterName = "@searchjurisdiction",
        Value = request.searchJurisdiction == null ? DBNull.Value : request.searchJurisdiction,
        NpgsqlDbType = NpgsqlDbType.Varchar
    },
    new NpgsqlParameter
    {
        ParameterName = "@searchorganization",
        Value = request.searchOrganization == null ? DBNull.Value : request.searchOrganization,
        NpgsqlDbType = NpgsqlDbType.Varchar
    },
    new NpgsqlParameter
    {
        ParameterName = "@searchaddress",
        Value = request.searchAddress == null ? DBNull.Value : request.searchAddress,
        NpgsqlDbType = NpgsqlDbType.Varchar
    },
    new NpgsqlParameter
    {
        ParameterName = "@searchcity",
        Value = request.searchCity == null ? DBNull.Value : request.searchCity,
        NpgsqlDbType = NpgsqlDbType.Varchar
    },
    new NpgsqlParameter
    {
        ParameterName = "@searchstate",
        Value = request.searchState == null ? DBNull.Value : request.searchState,
        NpgsqlDbType = NpgsqlDbType.Varchar
    },
    new NpgsqlParameter
    {
        ParameterName = "@searchzipcode",
        Value = request.searchZipCode == null ? DBNull.Value : request.searchZipCode,
        NpgsqlDbType = NpgsqlDbType.Varchar
    },
};


            string sql = "SELECT * FROM public.udf_searchfacilityresult(@identifier, @pagenumber, @pagesize, @sortby, @sortdirection, @searchfacilityname, @searchjurisdiction, @searchorganization, @searchaddress, @searchcity, @searchstate, @searchzipcode)";

            return (sql, functionParams);
        }
        
        #endregion

    }
}
