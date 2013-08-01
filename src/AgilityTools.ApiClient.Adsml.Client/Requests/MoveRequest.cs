using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
  public class MoveRequest : IAdsmlSerializable<XElement>
  {
    internal string SourcePath { get; private set; }
    internal string TargetPath { get; private set; }

    public IList<IMoveRequestFilter> RequestFilters { get; set; }
    public CopyControl CopyControl { get; set; }

    public MoveRequest(string source, string target) {
      if (source == null) 
        throw new ArgumentNullException("source");

      if (target == null) 
        throw new ArgumentNullException("target");

      SourcePath = source;
      TargetPath = target;

      this.RequestFilters = new List<IMoveRequestFilter>();
    }

    public XElement ToAdsml() {
      this.Validate();

      var request = 
        new XElement("MoveRequest",
         new XAttribute("name", this.SourcePath),
         new XAttribute("targetLocation", this.TargetPath));

      if (this.RequestFilters.Any()) {
        request.Add(this.RequestFilters.Select(rf => rf.ToAdsml()));
      }

      if (this.CopyControl != null) {
        request.AddFirst(this.CopyControl.ToAdsml());
      }

      return request;
    }

    private void Validate() {
      if (string.IsNullOrEmpty(this.SourcePath)) {
        throw new ApiSerializationValidationException("A source path must be provided.");
      }

      if(string.IsNullOrEmpty(this.TargetPath)) {
        throw new ApiSerializationValidationException("A target path must be provided.");
      }
    }
  }
}