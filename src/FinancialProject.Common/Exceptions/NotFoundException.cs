namespace FinancialProject.Common.Exceptions;

/// <summary>查無資料，或該資料不屬於目前使用者（擁有權檢查失敗）時拋出。</summary>
public sealed class NotFoundException : AppException
{
    public NotFoundException(string message) : base(message)
    {
    }
}
