using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
    public class AqlSearchRequest : IApiSerializable
    {
        public int ObjectTypeId { get; private set; }
        public int DefinitionIdToMatch { get; private set; }
        public string SearchTerm { get; private set; }
        public string AttributeName { get; private set; }
        public string BasePath { get; private set; }

        public bool OmitAttributes { get; set; }

        private readonly bool _matchWithDefinitionId;

        public AqlSearchRequest(int objectTypeId, int definitionIdToMatch, string searchTerm, 
                                string basePath = "/Structures/Classification/JULA Produkter") {

            this.ObjectTypeId = objectTypeId;
            this.DefinitionIdToMatch = definitionIdToMatch;
            this.SearchTerm = searchTerm;
            this.BasePath = basePath;

            this._matchWithDefinitionId = true;
        }

        public AqlSearchRequest(int objectTypeId, string attributeToMatch, string searchTerm, 
                                string basePath = "/Structures/Classification/JULA Produkter") {
            
            this.ObjectTypeId = objectTypeId;
            this.AttributeName = attributeToMatch;
            this.SearchTerm = searchTerm;
            this.BasePath = basePath;

            this._matchWithDefinitionId = false;
        }

        public XElement ToApiXml() {
            this.Validate();

            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            
            var request = _matchWithDefinitionId 
                ? new XElement("BatchRequest",
                                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                                new XElement("SearchRequest",
                                             new XAttribute("base", this.BasePath),
                                             new XElement("Filter",
                                                          new XElement("FilterString",
                                                                       string.Format(
                                                                           "FIND BELOW #{0} WHERE (#{1} = \"{2}\")",
                                                                           this.ObjectTypeId, this.DefinitionIdToMatch, this.SearchTerm))
                                                 )
                                    )
                )
                : new XElement("BatchRequest",
                                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                                new XElement("SearchRequest",
                                             new XAttribute("base", this.BasePath),
                                             new XElement("Filter",
                                                          new XElement("FilterString",
                                                                       string.Format(
                                                                           "FIND BELOW #{0} WHERE (\"{1}\" = \"{2}\")",
                                                                           this.ObjectTypeId, this.AttributeName, this.SearchTerm))
                                                 )
                                    )
                );

            if (this.OmitAttributes) {
                request.Descendants("SearchRequest").First().Add(new XAttribute("returnNoAttributes", "true"));
            }

            return request;
        }

        public void Validate() {
            if (string.IsNullOrEmpty(this.SearchTerm)) {
                throw new ApiSerializationValidationException("SearchTerm cannot be null or empty.");
            }

            if (this.ObjectTypeId == 0) {
                throw new ApiSerializationValidationException("ObjectTypeId must be set.");
            }

            if (!_matchWithDefinitionId) {
                if (string.IsNullOrEmpty(this.AttributeName)) {
                    throw new ApiSerializationValidationException("AttributeName cannot be null or empty.");
                }
            } else {
                if (this.DefinitionIdToMatch == 0) {
                    throw new ApiSerializationValidationException("DefinitionIdToMatch must be set.");
                }
            }
        }
    }
}