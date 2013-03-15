using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
  public class CreateRequest : IAdsmlSerializable<XElement>
  {
    public string ObjectTypeName { get; private set; }
    public string ContextName { get; private set; }
    public string ParentIdPath { get; set; }
    public IList<IAdsmlAttribute> AttributesToSet { get; private set; }
    
    public LookupControl LookupControl { get; set; }
    public IEnumerable<ICreateRequestFilter> RequestFilters { get; set; }
    
    public CreateRequest(string objectTypeName, string contextName, string parentIdPath, params IAdsmlAttribute[] attributesToSet) {
      if (string.IsNullOrEmpty(objectTypeName))
        throw new InvalidOperationException("ObjectTypeName cannot be null or empty.");
      
      if (string.IsNullOrEmpty(contextName))
        throw new InvalidOperationException("ContextName cannot be null or empty.");
      
      if (attributesToSet == null)
        throw new ArgumentNullException("attributesToSet");
      
      this.ObjectTypeName = objectTypeName;
      this.ContextName = contextName;
      this.ParentIdPath = parentIdPath;
      
      this.AttributesToSet = new List<IAdsmlAttribute>(attributesToSet);
    }
    
    public XElement ToAdsml() {
      var request = 
        new XElement("CreateRequest",
          new XAttribute("name", this.ContextName),
          new XAttribute("type", this.ObjectTypeName),
            new XElement(
              "AttributesToSet",
              this.AttributesToSet.Select(attrs => attrs.ToAdsml())));
      
      if (!string.IsNullOrEmpty(this.ParentIdPath)) {
        request.Descendants("CreateRequest").Single().Add(new XAttribute("parentIdPath", this.ParentIdPath));
      }
      
      if (this.RequestFilters != null)
        request.Descendants("CreateRequest").Single().Add(this.RequestFilters.Select(rf => rf.ToAdsml()));
      
      if (this.LookupControl != null)
        request.Descendants("CreateRequest").Single().AddFirst(this.LookupControl.ToAdsml());
      
      return request;
    }
  }
}