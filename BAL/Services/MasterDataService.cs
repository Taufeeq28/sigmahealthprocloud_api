using BAL.Interfaces;
using Data.Models;
using Data;
using Microsoft.Extensions.Logging;
using BAL.Constant;
using BAL.Request;
using Microsoft.EntityFrameworkCore;
using BAL.Responses;
using Npgsql;
using NpgsqlTypes;

namespace BAL.Services
{
    public class MasterDataService : DataAccessProvider<BusinessConfiguration>, IMasterDataService
    {
        private readonly SigmaproIisContext _dbContext;
        private readonly SigmaproIisContextUdf _dbContextudf;
        private readonly ILogger<MasterDataService> _logger;
        private readonly string _corelationId = string.Empty;
        public MasterDataService(SigmaproIisContext dbContext, ILogger<MasterDataService> logger, SigmaproIisContextUdf dbContextudf) : base(dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
            _dbContextudf = dbContextudf;
        }

        #region Public Methods
        public async Task<ApiResponse<GenerateNextIdResponse>> GenerateNextId(GenerateNextIdRequest request)
        {
            try
            {
                   
            
                if (String.IsNullOrEmpty(request.output_table_name) || String.IsNullOrEmpty(request.output_column_name) || String.IsNullOrEmpty(request.start_column_name) || String.IsNullOrEmpty(request.suffix_column_name))
                {
                    _logger.LogError($"CorelationId: {_corelationId} - Invalid Request Data");

                    throw new ArgumentException("Invalid Request Data.");
                }

                string sql = $"SELECT * FROM public.udf_generate_next_id('{request.output_table_name}','{request.suffix_column_name}','{request.start_column_name}', '{request.output_column_name}')";

                var result = await _dbContextudf.GenerateNextIdResults.FromSqlRaw(sql).FirstOrDefaultAsync();

                if (result != null)
                {
                    return ApiResponse<GenerateNextIdResponse>.Success(result, $"Next {request.output_column_name} generated successfully.");
                }
                else
                {
                    return ApiResponse<GenerateNextIdResponse>.Fail($"{request.output_column_name} generation failed.");
                }
            }
            catch (Exception exp)
            {

                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred during {request.output_column_name} generation in Method: {nameof(GenerateNextId)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                throw new Exception(exp?.Message);
               
            }
        }
#endregion

        #region Private Methods


        #endregion
    }
}
