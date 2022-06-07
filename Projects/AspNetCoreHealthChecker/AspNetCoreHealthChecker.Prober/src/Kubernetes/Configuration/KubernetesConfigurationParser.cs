// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics;
using System.Text.Json;

namespace AspNetCoreHealthChecker.Prober.Kubernetes.Configuration;

internal sealed class KubernetesConfigurationParser
{
  private readonly Dictionary<string, string> _data = new(StringComparer.OrdinalIgnoreCase);
  private readonly Stack<string> _paths = new();
  private bool _isSpec;
  private KubernetesConfigurationParser() { }

  public static IDictionary<string, string> Parse(String input)
  {
    return new KubernetesConfigurationParser().ParseString(input);
  }


  private IDictionary<string, string> ParseString(String input)
  {
    var jsonDocumentOptions = new JsonDocumentOptions
    {
      CommentHandling = JsonCommentHandling.Skip, AllowTrailingCommas = true
    };


    using (var doc = JsonDocument.Parse(input, jsonDocumentOptions))
    {
      if (doc.RootElement.ValueKind != JsonValueKind.Object)
      {
        // throw new FormatException(SR.Format(SR.Error_InvalidTopLevelJSONElement, doc.RootElement.ValueKind));
        throw new FormatException();
      }

      EnterContext("HealthCheck");
      VisitElement(doc.RootElement);
      ExitContext();
    }

    foreach (var s in _data)
    {
      Console.WriteLine(s);
    }

    return _data;
  }

  private void VisitElement(JsonElement element)
  {
    var isEmpty = true;

    foreach (var property in element.EnumerateObject())
    {
      isEmpty = false;
      if (property.Name == "items")
      {
        EnterContext("Probes");
        VisitValue(property.Value);
        ExitContext();
      }
      else if (property.Name == "metadata")
      {
        VisitValue(property.Value);
      }
      else if (property.Name == "name")
      {
        EnterContext("Name");
        VisitValue(property.Value);
        ExitContext();
      }
      else if (property.Name == "spec")
      {
        _isSpec = true;
        VisitValue(property.Value);
        _isSpec = false;
      }
      else if (_isSpec)
      {
        EnterContext(property.Name);
        VisitValue(property.Value);
        ExitContext();
      }
    }

    if (isEmpty && _paths.Count > 0)
    {
      _data[_paths.Peek()] = null;
    }
  }

  private void VisitValue(JsonElement value)
  {
    Debug.Assert(_paths.Count > 0);

    switch (value.ValueKind)
    {
      case JsonValueKind.Object:
        VisitElement(value);
        break;

      case JsonValueKind.Array:
        var index = 0;
        foreach (var arrayElement in value.EnumerateArray())
        {
          EnterContext(index.ToString());
          VisitValue(arrayElement);
          ExitContext();
          index++;
        }

        break;

      case JsonValueKind.Number:
      case JsonValueKind.String:
      case JsonValueKind.True:
      case JsonValueKind.False:
      case JsonValueKind.Null:
        var key = _paths.Peek();
        if (_data.ContainsKey(key))
        {
          // throw new FormatException(SR.Format(SR.Error_KeyIsDuplicated, key));
          throw new FormatException();
        }

        _data[key] = value.ToString();
        break;

      default:
        // throw new FormatException(SR.Format(SR.Error_UnsupportedJSONToken, value.ValueKind));
        throw new FormatException();
    }
  }

  private void EnterContext(string context)
  {
    _paths.Push(_paths.Count > 0 ? _paths.Peek() + ConfigurationPath.KeyDelimiter + context : context);
  }

  private void ExitContext()
  {
    _paths.Pop();
  }
}
