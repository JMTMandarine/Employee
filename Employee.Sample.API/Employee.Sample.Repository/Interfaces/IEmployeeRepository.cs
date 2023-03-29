using Employee.Sample.Data.ParamModels;
using Employee.Sample.Data.ResultModels;

namespace Employee.Sample.Services.Interfaces
{
    public interface IEmployeeRepository
    {
        public IEnumerable<EmployeeDBResult> GetEmployeeList(GetEmployeeListParam data);
        public EmployeeDBResult              GetEmployee(GetEmployeeParam data);
        public ResultModel                   InsertEmployees(EmployeeParam data);
    }
}
