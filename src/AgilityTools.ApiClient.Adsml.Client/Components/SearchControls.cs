using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class SearchControls : IAdsmlSerializable
    {
        public IList<ISearchControlComponent> SearchControlComponents { get; set; }
        public bool ExcludeResultsInBin { get; set; }
        public bool ExcludeResultsInDocumentFolder { get; set; }

        private XElement request;

        public SearchControls() : this (null) { }

        public SearchControls(params ISearchControlComponent[] searchControlComponents)
        {
            if (searchControlComponents != null) {
                this.SearchControlComponents = new List<ISearchControlComponent>(searchControlComponents);
            }
        }

        public XElement ToApiXml() {
            request = new XElement("SearchControls");
            
            this.ApplyFilters();

            foreach (var searchControlComponent in SearchControlComponents) {
                request.Add(searchControlComponent.ToApiXml());
            }

            return request;
        }

        private void ApplyFilters() {
            if (ExcludeResultsInBin)
                request.Add(new XAttribute("excludeBin", "true"));

            if (ExcludeResultsInDocumentFolder)
                request.Add(new XAttribute("excludeDocument", "true"));
        }

        public void Validate() { }
    }
}