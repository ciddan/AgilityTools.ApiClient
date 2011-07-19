using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Represents a LookupControls ADSML xml tag. Usually created by the user by utilizing a <see cref="LookupControlBuilder"/>.
    /// </summary>
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