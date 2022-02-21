namespace AspNetCoreHealthChecker;

public class ProbeResult
{
  public ProbeResultEnum ResultEnum { get; set; }

  public Exception? Exception { get; set; }

  public ProbeResult(ProbeResultEnum resultEnum)
  {
    ResultEnum = resultEnum;
    Exception = null;
  }

  public ProbeResult(ProbeResultEnum resultEnum, Exception exception)
  {
    ResultEnum = resultEnum;
    Exception = exception;
  }
}

public enum ProbeResultEnum
{
  Success,
  Failure,
  Timeout
}
