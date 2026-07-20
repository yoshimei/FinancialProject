namespace FinancialProject.Common.Exceptions;

/// <summary>輸入資料違反商業規則（如費率超出設定檔允許範圍）時拋出。</summary>
public sealed class ValidationException : AppException
{
    public ValidationException(string message) : base(message)
    {
    }
}
