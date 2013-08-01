using System;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
  public class DeleteRequest : IAdsmlSerializable<XElement>
  {
    internal string ContextToDelete { get; private set; }
    
    public DeleteRequest(string contextToDelete) {
      if (string.IsNullOrEmpty(contextToDelete)) {
        throw new InvalidOperationException("ContextToDelete must be set.");
      }
      
      this.ContextToDelete = contextToDelete;
    }
    
    public XElement ToAdsml() {
      return new XElement("DeleteRequest", new XAttribute("name", this.ContextToDelete));
    }
  }
}