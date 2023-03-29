using System.Data;

namespace Employee.Sample.Data.ParamModels
{
    public class EmployeeParam
    {
        public TvpEmployee tvpEmployee { get; set; }
        public EmployeeParam()
        { 
            tvpEmployee = new TvpEmployee();
        }
    }

    public class GetEmployeeListParam
    {
        public int page     { get; set; }
        public int pageSize { get; set; }
    }

    public class GetEmployeeParam
    {
        public string name { get; set; }
    }

    public class TvpEmployee
    {
        public DataTable data { get; set; }
        public TvpEmployee()
        {
            data = new DataTable();
            data.Columns.Add(new DataColumn("_name",   typeof(string)));
            data.Columns.Add(new DataColumn("_email",  typeof(string)));
            data.Columns.Add(new DataColumn("_tell",   typeof(string)));
            data.Columns.Add(new DataColumn("_joined", typeof(DateTime)));
        }
    }
}
