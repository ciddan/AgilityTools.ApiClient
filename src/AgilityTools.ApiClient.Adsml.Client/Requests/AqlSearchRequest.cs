using System.Text;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
    public class AqlSearchRequest : IAdsmlSerializable<XElement>
    {
        internal string BasePath { get; private set; }     
        internal string QueryType { get; private set; }
        internal string ObjectTypeToFind { get; private set; }
        internal string QueryString { get; private set; }
        internal SearchControl SearchControl { get; private set; }

        public bool OmitStructureAttributes { get; set; }

        private string _aqlFind;

        internal AqlSearchRequest(string basePath, QueryTypes queryType, IdNameReference typeToFind, string queryString, SearchControl searchControl) {
            this.BasePath = basePath;
            this.QueryType = queryType.GetStringValue();
            this.QueryString = queryString;
            this.SearchControl = searchControl;

            if (typeToFind != null) {
                this.ObjectTypeToFind = typeToFind.ToString();
            }
        }

        public XElement ToAdsml() {
            _aqlFind = BuildAqlFind();
            
            this.Validate();

            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";

            var request = 
                new XElement("BatchRequest",
                    new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                    new XElement("SearchRequest",
                        new XElement("Filter",
                            new XElement("FilterString",
                                string.Format(
                                    "{0}WHERE ({1})",
                                    _aqlFind, this.QueryString)))));

            if (!string.IsNullOrEmpty(this.BasePath))
                request.Descendants("SearchRequest").Single().Add(new XAttribute("base", this.BasePath));

            if (OmitStructureAttributes)
                request.Descendants("SearchRequest").Single().Add(new XAttribute("returnNoAttributes", "true"));

            if (this.SearchControl != null)
                request.Descendants("SearchRequest").Single().AddFirst(this.SearchControl.ToAdsml());

            return request;
        }

        public void Validate() {
            if (string.IsNullOrEmpty(this.QueryString)) {
                throw new ApiSerializationValidationException("An AQL QueryString must be provided.");
            }

            if (string.IsNullOrEmpty(this.BasePath) && this.QueryType != string.Empty) {
                throw new ApiSerializationValidationException("To use a specific QueryType the base path must be provided.");
            }
        }

        private string BuildAqlFind() {
            var sb = new StringBuilder();

            sb.Append("FIND ");

            if (!string.IsNullOrEmpty(this.QueryType)) {
                sb.AppendFormat("{0} ", this.QueryType);
            }

            if(!string.IsNullOrEmpty(this.ObjectTypeToFind)) {
                sb.AppendFormat("{0} ", this.ObjectTypeToFind);
            } else {
                sb.Append("ANY ");
            }

            return sb.ToString();
        }
    }
}