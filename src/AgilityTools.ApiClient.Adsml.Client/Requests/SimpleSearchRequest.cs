using System;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client
{
  public class SimpleSearchRequest : IAdsmlSerializable<XElement>
  {
    private readonly string baseContext;
    private readonly SearchControl searchControls;

    public SimpleSearchRequest(string baseContext) : this(baseContext, null) {}

    public SimpleSearchRequest(string baseContext, SearchControl searchControls) {
      if (baseContext == null)
        throw new ArgumentNullException("baseContext");
      
      if (string.IsNullOrEmpty(baseContext))
        throw new ArgumentException("String cannot be empty.", "baseContext");
      
      this.baseContext = baseContext;
      this.searchControls = searchControls;
    }

    public XElement ToAdsml() {
      var request = new XElement("SearchRequest", new XAttribute("base", this.baseContext));
      
      if (this.searchControls != null) {
        request.AddFirst(this.searchControls.ToAdsml());
      }
      
      return request;
    }
  }
}

