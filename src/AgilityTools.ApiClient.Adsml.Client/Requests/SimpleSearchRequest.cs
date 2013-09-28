using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client
{
  public class SimpleSearchRequest : IAdsmlSerializable<XElement>
  {
    public IList<ISearchRequestFilter> RequestFilters { get; private set; }

    private readonly string baseContext;
    private readonly SearchControl searchControls;

    public SimpleSearchRequest(string baseContext) : this(baseContext, null) {}

    public SimpleSearchRequest(string baseContext, SearchControl searchControls) {
      this.RequestFilters = new List<ISearchRequestFilter>();

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

      if (this.RequestFilters.Count > 0) {
        request.Add(this.RequestFilters.Select(f => f.ToAdsml()));
      }
      
      return request;
    }
  }
}

