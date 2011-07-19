using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Used to set parameters for a copying operation.
    /// </summary>
    public class CopyControl : ControlBase
    {
        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="requestFilters">Optional. If specified, the filter(s) are added to the final serialized ADSML xml.</param>
        /// <param name="lookupControl">Optional. If specified, the lookup control is added to the final serialized ADSML xml.</param>
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