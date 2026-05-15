namespace ProductStoreMVC;

public class Result
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }

    public static Result Fail(string errorMessage)
        => new Result { IsSuccess = false, ErrorMessage = errorMessage };
    public static Result Success()
        => new Result { IsSuccess = true };
}
