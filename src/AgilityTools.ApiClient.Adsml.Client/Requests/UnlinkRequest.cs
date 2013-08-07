using System;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
  public class UnlinkRequest : IAdsmlSerializable<XElement>
  {
    internal string TargetPath { get; private set; }

    public UnlinkRequest(string name) {
      if (string.IsNullOrEmpty(name)) {
        throw new ArgumentException("Value cannot be null or empty.", "name");
      }

      this.TargetPath = name;
    }

    public XElement ToAdsml() {
      return new XElement("UnlinkRequest", new XAttribute("name", this.TargetPath));
    }
  }
}

