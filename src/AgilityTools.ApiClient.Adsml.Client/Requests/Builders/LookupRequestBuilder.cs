using System;
using System.Linq;
using System.Collections.Generic;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
  public class LookupRequestBuilder : ILookupRequestBuilder
  {
    internal string Name { get; private set; }
    internal LookupControlBuilder LookupControlBuilder { get; private set; }
    internal List<ILookupRequestFilter> Filters { get; private set; }

    public LookupRequestBuilder() {
      this.Filters = new List<ILookupRequestFilter>();
    }

    public IConfigLookupControlsLookupReturnNoAttributesLookup Context(string name) {
      this.Name = name;

      if (string.IsNullOrEmpty(name)) {
        throw new InvalidOperationException("name cannot be null or empty");
      }

      return this;
    }

    public IConfigLookupControlsLookup ReturnNoAttributes(bool returnNoAttributes = true) {
      this.Filters.Add(new ReturnNoAttributesFilter(returnNoAttributes));
      return this;
    }

    public ILookupControlBuilder ConfigureLookupControls() {
      this.LookupControlBuilder = new LookupControlBuilder();

      return this.LookupControlBuilder;
    }

    public LookupRequest Build() {
      var request = this.LookupControlBuilder != null
        ? new LookupRequest(this.Name, this.LookupControlBuilder.Build()) 
        : new LookupRequest(this.Name);

      if (this.Filters.Any()) {
        request.RequestFilters = this.Filters;
      }

      return request;
    }
  }
}