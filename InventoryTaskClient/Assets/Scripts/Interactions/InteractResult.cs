/// <summary>
/// The result of an interaction. If it is a failure it also contains an error message for the player.
/// </summary>
public class InteractResult
{
    protected enum InteractResultType { Success, Failure }

    public static InteractResult Success { get; } = new InteractResult(InteractResultType.Success, string.Empty);
    public static InteractResult Fail    { get; } = new InteractResult(InteractResultType.Failure, string.Empty);
    
    public string Message { get; }
    private InteractResultType Type { get; }

    public bool IsSuccess => this.Type == InteractResultType.Success;
    public bool IsFailure => this.Type == InteractResultType.Failure;

    protected InteractResult(InteractResultType result, string message)
    {
        this.Type = result;
        this.Message = message;
    }
}