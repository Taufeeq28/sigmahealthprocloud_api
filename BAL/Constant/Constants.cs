
using Microsoft.Extensions.Logging;

namespace BAL
{
    public static class Constants
    {
        public const string OUTPUT_CLOUMN_NAME_FACILITY_ID = "facility_id";

        public const string START_CLOUMN_NAME_FACILITY_ID_START = "facilty_id_start";

        public const string SUFFIX_CLOUMN_NAME_FACILITY_ID_SUFFIX = "facilty_id_suffix";

        public const string OUTPUT_TABLE_NAME_FACILITIES = "facilities";

        public const string OUTPUT_CLOUMN_NAME_ADDRESS_ID = "address_id";

        public const string START_CLOUMN_NAME_ADDRESS_ID_START = "address_id_start";

        public const string SUFFIX_CLOUMN_NAME_ADDRESS_ID_SUFFIX = "address_id_suffix";

        public const string OUTPUT_TABLE_NAME_ADDRESSES = "addresses";

        public static ILogger<T> CreateLogger<T>()
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            return loggerFactory.CreateLogger<T>();
        }
    }
    public static class ApiResponsesConstants
    {
        public const string SUCCESS_STATUS = "Success";
        public const string FAILURE_STATUS = "Failure";
        public const string SUCCESS_STATUS_CODE = "200";
        public const string FAILURE_STATUS_CODE = "400";

    }
}
