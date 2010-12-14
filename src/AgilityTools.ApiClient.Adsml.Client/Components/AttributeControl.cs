using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public class AttributeControl : ControlComponentBase<IAttributeControl>, ISearchControlComponent, ILookupControlComponent
    {
        internal AttributeControl(params IAttributeControl[] attributesToReturn) {
            if (attributesToReturn == null)
                throw new ArgumentNullException("attributesToReturn");

            this.OuterNodeAttributes = new List<XAttribute>();
            this.ContentNodes = new List<IAttributeControl>(attributesToReturn);
            this.NodeName = "AttributesToReturn";
        }

        internal void AddAttributes(params IAttributeControl[] attributesToReturn) {
            this.ContentNodes = new List<IAttributeControl>(attributesToReturn);
        }
    }
}