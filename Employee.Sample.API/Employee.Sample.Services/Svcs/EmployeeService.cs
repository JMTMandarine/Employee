using Employee.Sample.Core.Models;
using Employee.Sample.Data.DataModels;
using Employee.Sample.Data.ParamModels;
using Employee.Sample.Data.ResultModels;
using Employee.Sample.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace Employee.Sample.Services.Svcs
{
    public class EmployeeService : IEmployee
    {
        private IEmployeeRepository _repo;
        public EmployeeService(IEmployeeRepository repo) 
        {
            _repo = repo;
        }

        public EmployeeInfo GetEmployeeByName(string name)
        {
            EmployeeDBResult    dbResult = new EmployeeDBResult();
            EmployeeInfo        result   = new EmployeeInfo();
            GetEmployeeParam    param    = new GetEmployeeParam()
            {
                name = name
            };

            dbResult = _repo.GetEmployee(param);

            if(null == dbResult)
            {
                throw new CustomException($"not exist name employee : [{name}]");
            }

            result.name     = dbResult._name;
            result.email    = dbResult._email;
            result.tel      = dbResult._tel;
            result.joined   = dbResult._joined;

            return result;
        }

        public IEnumerable<EmployeeInfo> GetEmployeeList(PagingData data)
        {
            IEnumerable<EmployeeDBResult> dbResult  = new List<EmployeeDBResult>();
            IEnumerable<EmployeeInfo>     result    = new List<EmployeeInfo>();
            GetEmployeeListParam          param     = new GetEmployeeListParam()
            {
                page      = data.page
                ,pageSize = data.pageSize
            };
            dbResult = _repo.GetEmployeeList(param);

            if(null != dbResult && dbResult.Count() > 0)
            {
                result = dbResult.Select(x=> new EmployeeInfo() { 
                            name    = x._name
                            ,email  = x._email
                            ,tel    = x._tel
                            ,joined = x._joined
                        });
            }

            return result;
        }

        public ResultModel InsertEmployees(IFormFile? file, string? jsonString)
        {
            ResultModel result = new ResultModel();
            EmployeeParam param = new EmployeeParam();
            List<EmployeeData> employeeDatas = new List<EmployeeData>();
            employeeDatas = GetObjectDatas(file, jsonString);

            foreach(var item in employeeDatas)
            {
                param.tvpEmployee.data.Rows.Add(item.name, item.email, item.tel, item.joined);
            }

            result = _repo.InsertEmployees(param);

            if (0 != result.resultCode)
            {
                throw new CustomException($"[Insert Employees Error] : [{result.resultMsg}]");
            }

            return result;
        }

        #region private
        private List<EmployeeData> GetObjectDatas(IFormFile? file, string? jsonString)
        {
            List<EmployeeData> result    = new List<EmployeeData>();
            string?  fileContent         = jsonString;
            string[] acceptFileExtension = {".csv", ".json"};
            string   fileName            = string.Empty;
            string   fileExtension       = string.Empty;
            string[] lines;
            string[] fields;

            /* file이 존재한다면 jsonString 덮음 : upload file 우선 처리(변경가능) */
            if (file != null && file.Length > 0)
            {
                fileName      = Path.GetFileName(file.FileName);
                fileExtension = Path.GetExtension(fileName);

                if(false == acceptFileExtension.Contains(fileExtension))
                {
                    throw new CustomException("Error : Invalid file type");
                }

                using (StreamReader reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
                {
                    fileContent = reader.ReadToEnd();
                }    
            }

            /* fileContent Check */
            if (string.IsNullOrWhiteSpace(fileContent))
            {
                throw new CustomException("Error : Invalid file type");
            }

            /* csv parsing */
            if (fileExtension.Equals(".csv"))
            {
                lines = fileContent.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string line in lines)
                {
                    fields = line.Split(',');
                    result.Add(new EmployeeData()
                    {
                        name   = fields[0],
                        email  = fields[1],
                        tel    = fields[2],
                        joined = Convert.ToDateTime(fields[3])
                    });
                }
            }
            /* json parsing */
            else
            {
                result = JsonConvert.DeserializeObject<List<EmployeeData>>(fileContent);
            }

            return result;
        }
        #endregion
    }
}
