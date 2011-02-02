using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public class SearchControl : ControlBase
    {
        internal SearchControl(IEnumerable<ISearchRequestFilter> requestFilters, IEnumerable<ISearchControlComponent> components) {
            if (requestFilters != null)
                this.RequestFilters = new List<IRequestFilter>(requestFilters);

            if (components != null)
                this.Components = new List<IControlComponent>(components);
            
            this.OuterNodeAttributes = new List<XAttribute>();
            this.NodeName = "SearchControls";
        }
    }
}