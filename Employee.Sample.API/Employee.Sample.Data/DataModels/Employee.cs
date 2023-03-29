namespace Employee.Sample.Data.DataModels
{
    public class EmployeeData
    {
        public string? email  { get; set; }
        public string? name   { get; set; }
        public string? tel    { get; set; }
        public DateTime joined { get; set; }
    }
    public class PagingData
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public PagingData()
        {
            page     = 1;
            pageSize = 30;
        }
    }
}
