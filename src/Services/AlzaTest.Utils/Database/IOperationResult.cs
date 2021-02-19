namespace AlzaTest.Services.Utils.Database
{
    public interface IOperationResult
    {
        bool IsValid { get; }
        
        string ErrorMessage { get; }
        
        int ErrorCode { get; }
    }
}