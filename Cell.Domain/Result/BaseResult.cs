namespace Cell.Domain.Result;
public class BaseResult<T>
{
    public BaseResult() { }
    public bool IsSuccess => String.IsNullOrEmpty(ErrorMassage);

    public string ErrorMassage { get; set; }

    public int? ErrorCode { get; set; }

    public T Data { get; set; }
}