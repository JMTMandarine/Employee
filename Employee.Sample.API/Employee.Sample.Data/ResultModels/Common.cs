namespace Employee.Sample.Data.ResultModels
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
    }

    public class ResultModel
    {
        public int resultCode { get; set; }
        public string resultMsg { get; set; }
        public void Success()
        {
            resultCode = 0;
            resultMsg = string.Empty;
        }

        public void Fail()
        {
            resultCode = -99;
            resultMsg = "Exception Error";
        }

        public ResultModel()
        {
            resultCode = -99;
            resultMsg = "Error";
        }
    }
}
