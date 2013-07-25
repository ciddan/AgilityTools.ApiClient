using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
  public class LookupRequest : IAdsmlSerializable<XElement>
  {
    internal string Name { get; private set; }
    internal LookupControl LookupControls { get; private set; }
    public IEnumerable<ILookupRequestFilter> RequestFilters { get; set; }
    
    public LookupRequest(string name, LookupControl lookupControls = null, params ILookupRequestFilter [] filters) {
      Name = name;
      LookupControls = lookupControls;
      
      if (string.IsNullOrEmpty(name))
        throw new InvalidOperationException("name cannot be null or empty.");
      
      this.Name = name;
      this.LookupControls = lookupControls;

      if (filters != null && filters.Any()) {
        this.RequestFilters = filters;
      }
    }
    
    public XElement ToAdsml() {
      var request = new XElement("LookupRequest", new XAttribute("name", this.Name));
      
      if (this.LookupControls != null) {
        request.AddFirst(this.LookupControls.ToAdsml());
      }

      if (this.RequestFilters != null)
        request.Add(this.RequestFilters.Select(rf => rf.ToAdsml()));
      
      return request;
    }
  }
}