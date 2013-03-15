using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
  public class BatchRequestBuilder : IRequestBuilder<BatchRequest>
  {
    private IList<IRequestBuilder<IAdsmlSerializable<XElement>>> _builders;

    public BatchRequestBuilder() {
      this._builders = new List<IRequestBuilder<IAdsmlSerializable<XElement>>>();
    }

    public void AddRequests(params IRequestBuilder<IAdsmlSerializable<XElement>>[] builders) {
      foreach (var builder in builders)
        this._builders.Add(builder);
    }

    public BatchRequest Build() {
      return new BatchRequest(_builders.Select(b => b.Build()).ToArray());
    }
  }
}
