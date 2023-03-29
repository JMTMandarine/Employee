namespace Employee.Sample.Data.ResultModels
{
    /* retrun 용 */
    public class EmployeeInfo
    {
        public string name   { get; set; }
        public string tel    { get; set; }
        public string email  { get; set; }
        public string joined { get; set; }
    }

    /* db 용 */
    public class EmployeeDBResult
    {
        public string _name   { get; set; }
        public string _tel    { get; set; }
        public string _email  { get; set; }
        public string _joined { get; set; }
    }
}
