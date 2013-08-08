using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client
{
  public class RenameRequest : IAdsmlSerializable<XElement>
  {
    internal string Context { get; private set; }
    internal string NewName { get; private set; }

    internal IList<IRenameRequestFilter> RequestFilters { get; private set; }

    public RenameRequest(string context, string newName) {
      if (string.IsNullOrEmpty(context)) {
        throw new ArgumentException("Value cannot be null or empty", "context");
      }

      if (string.IsNullOrEmpty(newName)) {
        throw new ArgumentException("Value cannot be null or empty", "newName");
      }

      this.Context = context;
      this.NewName = newName;
      this.RequestFilters = new List<IRenameRequestFilter>();
    }

    public XElement ToAdsml() {
      this.Validate();

      var request = 
        new XElement("RenameRequest",
                     new XAttribute("name", this.Context),
                     new XAttribute("newName", this.NewName));

      if (this.RequestFilters.Any()) {
        request.Add(this.RequestFilters.Select(rf => rf.ToAdsml()));
      }

      return request;
    }

    private void Validate() {
      if (string.IsNullOrEmpty(this.Context)) {
        throw new ApiSerializationValidationException("A source context must be provided.");
      }

      if(string.IsNullOrEmpty(this.NewName)) {
        throw new ApiSerializationValidationException("New context name must be provided.");
      }
    }
  }
}

