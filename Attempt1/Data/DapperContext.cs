using Microsoft.Data.SqlClient;

namespace Attempt1.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("There is no con string inside in this content");
        }

        public SqlConnection CreateConnection()
            => new SqlConnection(_connectionString);
        //public SqlConnection CreateConnection()
        //{
        //    return new SqlConnection(_connectionString);
        //}

    }
}
