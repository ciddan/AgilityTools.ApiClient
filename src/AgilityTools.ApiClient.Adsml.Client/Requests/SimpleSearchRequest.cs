using System;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client
{
  public class SearchRequest : IAdsmlSerializable<XElement>
  {
    private readonly string baseContext;
    private readonly SearchControl searchControls;

    public SearchRequest(string baseContext) : this(baseContext, null) {}

    public SearchRequest(string baseContext, SearchControl searchControls) {
      if (baseContext == null)
        throw new ArgumentNullException("baseContext");
      
      if (string.IsNullOrEmpty(baseContext))
        throw new ArgumentException("String cannot be empty.", "baseContext");
      
      this.baseContext = baseContext;
      this.searchControls = searchControls;
    }

    public XElement ToAdsml() {
      throw new NotImplementedException();
    }
  }
}

