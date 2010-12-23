using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using AgilityTools.ApiClient.Adsml.Client.Filters;
using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
    public class CreateRequest : IAdsmlSerializable<XElement>
    {
        internal string ObjectTypeName { get; private set; }
        internal string CreationPath { get; private set; }
        internal IList<IAdsmlAttribute> AttributesToSet { get; private set; }

        public LookupControl LookupControl { get; set; }
        public IEnumerable<ICreateRequestFilter> RequestFilters { get; set; }

        public CreateRequest(string objectTypeName, string creationPath, params IAdsmlAttribute[] attributesToSet) {
            if (string.IsNullOrEmpty(objectTypeName))
                throw new InvalidOperationException("ObjectTypeName cannot be null or empty.");

            if (string.IsNullOrEmpty(creationPath))
                throw new InvalidOperationException("CreationPath cannot be null or empty.");

            if (attributesToSet == null)
                throw new ArgumentNullException("attributesToSet");

            this.ObjectTypeName = objectTypeName;
            this.CreationPath = creationPath;

            this.AttributesToSet = new List<IAdsmlAttribute>(attributesToSet);
        }

        public XElement ToAdsml() {
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var request = new XElement("BatchRequest",
                                        new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                                        new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                                        new XElement("CreateRequest",
                                                     new XAttribute("name", this.CreationPath),
                                                     new XAttribute("type", this.ObjectTypeName),
                                                     new XElement("AttributesToSet",
                                                                  this.AttributesToSet.Select(attrs => attrs.ToAdsml()))));

            if (this.RequestFilters != null)
                request.Descendants("CreateRequest").Single().Add(this.RequestFilters.Select(rf => rf.ToAdsml()));

            if (this.LookupControl != null)
                request.Descendants("CreateRequest").Single().AddFirst(this.LookupControl.ToAdsml());

            return request;
        }
    }
}