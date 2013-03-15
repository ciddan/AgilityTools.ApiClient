using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
  public class LinkRequest : IAdsmlSerializable<XElement>
  {
    internal string SourcePath { get; private set; }
    internal string TargetPath { get; private set; }
    
    public IList<ILinkRequestFilter> RequestFilters { get; set; }
    public CopyControl CopyControl { get; set; }
    
    public LinkRequest(string source, string target) {
      if (source == null) 
        throw new ArgumentNullException("source");
      
      if (target == null) 
        throw new ArgumentNullException("target");
      
      SourcePath = source;
      TargetPath = target;
      
      this.RequestFilters = new List<ILinkRequestFilter>();
    }
    
    public XElement ToAdsml() {
      this.Validate();
      
      XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
      
      var request = 
        new XElement("LinkRequest",
          new XAttribute("name", this.SourcePath),
          new XAttribute("targetLocation", this.TargetPath));
      
      if (this.RequestFilters.Count() >= 1 ) {
        request.Descendants("LinkRequest").Single().Add(this.RequestFilters.Select(rf => rf.ToAdsml()));
      }
      
      if (this.CopyControl != null) {
        request.Descendants("LinkRequest").Single().AddFirst(this.CopyControl.ToAdsml());
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