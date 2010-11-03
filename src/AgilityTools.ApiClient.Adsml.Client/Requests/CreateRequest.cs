using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
    public class CreateRequest : IApiSerializable
    {
        public string ObjectTypeName { get; private set; }
        public string CreationPath { get; private set; }
        public IList<StructureAttribute> AttributesToSet { get; private set; }

        public CreateRequest(string objectTypeName, string creationPath, IList<StructureAttribute> attributesToSet) {
            if (string.IsNullOrEmpty(objectTypeName))
                throw new InvalidOperationException("ObjectTypeName cannot be null or empty.");

            if (string.IsNullOrEmpty(creationPath))
                throw new InvalidOperationException("CreationPath cannot be null or empty.");

            if (attributesToSet == null)
                throw new ArgumentNullException("attributesToSet");

            this.ObjectTypeName = objectTypeName;
            this.AttributesToSet = attributesToSet;
            this.CreationPath = creationPath;
        }

        public XElement ToApiXml() {
            this.Validate();

            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            return new XElement("BatchRequest",
                                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                                new XElement("CreateRequest",
                                             new XAttribute("name", this.CreationPath),
                                             new XAttribute("type", this.ObjectTypeName),
                                             new XElement("AttributesToSet",
                                                          this.AttributesToSet.Select(attrs => attrs.ToApiXml()))
                                    ));
        }

        public void Validate() {
            // No validation is necessary here since all params must be set in ctor, 
            // and are enforced by safeguarding checks.    
        }
    }
}