using Dapper;
using Employee.Sample.Data.ParamModels;
using Employee.Sample.Data.ResultModels;
using Employee.Sample.Services.Interfaces;
using System.Data;

namespace Employee.Sample.Repository.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        /* 사원 리스트 - 페이징 */
        public IEnumerable<EmployeeDBResult> GetEmployeeList(GetEmployeeListParam data)
        {
            IEnumerable<EmployeeDBResult>   result = new List<EmployeeDBResult>();
            DynamicParameters               param  = new DynamicParameters();

            param.Add("page",       data.page,      DbType.Int64, ParameterDirection.Input);
            param.Add("pageSize",   data.pageSize,  DbType.Int64, ParameterDirection.Input);
            using (IDbConnection conn = _context.Connection())
            {
                result = conn.Query<EmployeeDBResult>("uspGetEmployeeList", param, commandType: CommandType.StoredProcedure).ToList();
            }
            return result;
        }

        /* 사원 정보 단일 - 이름 검색 */
        public EmployeeDBResult GetEmployee(GetEmployeeParam data) 
        {
            DynamicParameters param = new DynamicParameters();
            EmployeeDBResult result = new EmployeeDBResult();

            param.Add("name", data.name, DbType.String, ParameterDirection.Input);
            using (IDbConnection conn = _context.Connection())
            {
                result = conn.Query<EmployeeDBResult>("uspGetEmployee", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return result;
        }

        /* 사원 정보 입력 - tvp를 사용한 insert */
        public ResultModel InsertEmployees(EmployeeParam data)
        {
            ResultModel       result = new ResultModel();
            DynamicParameters param  = new DynamicParameters();
          
            param.Add("employeeList", data.tvpEmployee.data.AsTableValuedParameter("TblTypeEmployee"));

            param.Add("resultCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("resultMsg",  dbType: DbType.String, direction: ParameterDirection.Output, size: 1000);
            using (IDbConnection conn = _context.Connection())
            {
                conn.Execute("uspInsertEmployee", param, commandType: CommandType.StoredProcedure);
                result.resultCode = param.Get<int>("resultCode");
                result.resultMsg = param.Get<string>("resultMsg");
            }

            return result;
        }
    }
}