using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
  public class BatchRequest : IAdsmlSerializable<XElement>
  {
    public IList<IAdsmlSerializable<XElement>> Requests { get; set; }

    public BatchRequest() {
      this.Requests = new List<IAdsmlSerializable<XElement>>();
    }

    public BatchRequest(params IAdsmlSerializable<XElement>[] requests) {
      this.Requests = new List<IAdsmlSerializable<XElement>>();
      foreach (var request in requests) this.Requests.Add(request);
    }

    public XElement ToAdsml() {
      XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
      
      var request =
        new XElement("BatchRequest",
          new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
          new XAttribute(XNamespace.Xmlns + "xsi", xsi),
            this.Requests.Select(r => r.ToAdsml()));

      return request;
    }
  }
}
