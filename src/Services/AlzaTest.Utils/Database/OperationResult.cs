namespace AlzaTest.Services.Utils.Database
{
    public class OperationResult : IOperationResult
    {
        public bool IsValid { get; set; }
        
        public string ErrorMessage { get; set; }
        
        public int ErrorCode { get; set; }
    }
}