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
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var request = new XElement("BatchRequest",
                                       new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                                       new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                                       new XElement("DeleteRequest",
                                                    new XAttribute("name", this.ContextToDelete)));

            return request;
        }
    }
}