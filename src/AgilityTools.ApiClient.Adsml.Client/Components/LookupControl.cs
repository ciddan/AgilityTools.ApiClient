using System.Collections.Generic;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Filters;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public class LookupControl : ControlBase
    {
        internal LookupControl(IEnumerable<ILookupControlFilter> requestFilters, IEnumerable<ILookupControlComponent> components) {
            if (requestFilters != null)
                this.RequestFilters = new List<IRequestFilter>(requestFilters);

            if (components != null)
                this.Components = new List<IControlComponent>(components);
            
            this.OuterNodeAttributes = new List<XAttribute>();
            this.NodeName = "LookupControls";
        }
    }
}