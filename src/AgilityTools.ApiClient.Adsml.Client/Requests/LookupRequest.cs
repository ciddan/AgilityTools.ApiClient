using System;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
    public class LookupRequest : IAdsmlSerializable<XElement>
    {
        internal string Name { get; private set; }
        internal LookupControl LookupControls { get; private set; }

        public LookupRequest(string name, LookupControl lookupControls = null) {
            Name = name;
            LookupControls = lookupControls;
            
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("name cannot be null or empty.");
            
            this.Name = name;
            this.LookupControls = lookupControls;
        }

        public XElement ToAdsml() {
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";

            var request = new XElement("BatchRequest",
                                        new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                                        new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                                        new XElement("LookupRequest",
                                                     new XAttribute("name", this.Name)));

            if (this.LookupControls != null) {
                request.Descendants("LookupRequest").Single().AddFirst(this.LookupControls.ToAdsml());
            }

            return request;
        }
    }
}