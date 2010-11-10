using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class SearchControl : IAdsmlSerializable<XElement>
    {
        private readonly IList<ISearchRequestFilter> _requestFilters;
        private readonly IList<ISearchControlComponent> _components;
  
        private XElement request;

        internal SearchControl(IList<ISearchRequestFilter> requestFilters, IList<ISearchControlComponent> components) {
            _requestFilters = requestFilters;
            _components = components;
        }

        public XElement ToAdsml() {
            request = new XElement("SearchControls");
            
            this.ApplyFilters();

            if (this._components != null) {
                foreach (var searchControlComponent in this._components) {
                    request.Add(searchControlComponent.ToAdsml());
                }
            }

            return request;
        }

        private void ApplyFilters() {
            if (this._requestFilters != null) {
                request.Add(this._requestFilters.Select(filter => filter.ToAdsml()));
            }
        }

        public void Validate() { }
    }
}