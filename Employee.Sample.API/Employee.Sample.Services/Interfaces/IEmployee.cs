using Employee.Sample.Data.DataModels;
using Employee.Sample.Data.ResultModels;
using Microsoft.AspNetCore.Http;

namespace Employee.Sample.Services.Interfaces
{
    public interface IEmployee
    {
        public EmployeeInfo                 GetEmployeeByName(string name);
        public IEnumerable<EmployeeInfo>    GetEmployeeList(PagingData data);
        public ResultModel                  InsertEmployees(IFormFile? file, string? jsonString);
    }
}
