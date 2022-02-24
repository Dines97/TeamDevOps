using Newtonsoft.Json;

namespace AspNetCoreHealthChecker;

public class ProbeRunner
{
  private readonly List<ProbeDelegate> _probeDelegates;

  public ProbeRunner(List<ProbeDelegate> probeDelegates)
  {
    this._probeDelegates = probeDelegates;
  }

  public string RunJson()
  {
    _probeDelegates.ForEach(p => p.Run());

    return JsonConvert.SerializeObject(_probeDelegates);
  }
}
