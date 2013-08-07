using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client
{
  public class SortChildrenRequest : IAdsmlSerializable<XElement>
  {
    internal string Target { get; private set; }
    public IList<ISortRequestFilter> RequestFilters { get; set; }

    public SortChildrenRequest(string target) {
      if (target == null) 
        throw new ArgumentNullException("target");

      this.Target = target;
      this.RequestFilters = new List<ISortRequestFilter>();
    }

    public XElement ToAdsml() {
      this.Validate();

      var request = 
        new XElement("SortChildrenRequest",
                     new XAttribute("name", this.Target));

      if (this.RequestFilters.Any()) {
        request.Add(this.RequestFilters.Select(rf => rf.ToAdsml()));
      }

      return request;
    }

    private void Validate() {
      if(string.IsNullOrEmpty(this.Target)) {
        throw new ApiSerializationValidationException("A target path must be provided.");
      }
    }
  }
}

