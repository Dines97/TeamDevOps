# Demo

```csharp

public interface IDemoAction
{
  bool Check(string text);
  void Run();
}

public class ADemoAction : IDemoAction
{
  public bool Check(string text)
  {
    var t = text?.ToUpper();
    return text == "A" || text == "B";
  }

  public void Run()
  {
    throw new NotImplementedException();
  }
}


var map = new List<IDemoAction>()
{
  new ADemoAction(),
  new ADemoAction()
};


Console.WriteLine("Seçiniz : A, B, C, D, E"); // a
var a = Console.ReadLine();

//var b = map[a];

//b.Run();

foreach (var t in map)
{
  if (t.Check(a))
  {
    t.Run();
    break;
  }
}




if (a == "A" || a == "B")
{
  Console.WriteLine("A seçtiniz.");
}
else if (a == "C")
{
  Console.WriteLine("C seçtiniz.");
}
else if (a == "D")
{
  Console.WriteLine("D seçtiniz.");
}
else if (a == "E")
{
  Console.WriteLine("E seçtiniz.");
}


```