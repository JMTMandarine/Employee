using Employee.Sample.Data.ResultModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Employee.Sample.Repository.Repository
{
    public class EmployeeDbContext
    {
        private readonly AppSettings _appSettings;
        public EmployeeDbContext(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public IDbConnection Connection()
            => new SqlConnection(_appSettings.ConnectionString);
    }
}