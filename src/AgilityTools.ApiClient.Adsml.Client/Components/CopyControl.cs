using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public class CopyControl : ControlBase
    {
        internal CopyControl(IEnumerable<ICopyControlFilter> requestFilters, LookupControl lookupControl = null) {
            
            if (requestFilters != null) {
                this.RequestFilters = new List<IRequestFilter>(requestFilters);
            }

            if (lookupControl != null) {
                this.Components = new List<IControlComponent> {lookupControl};
            }

            this.OuterNodeAttributes = new List<XAttribute>();
            this.NodeName = "CopyControls";
        }
    }
}