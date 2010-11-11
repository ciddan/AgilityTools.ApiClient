using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class AttributeControl : SearchControlComponentBase<IAttributeControl>
    {
        internal AttributeControl(params IAttributeControl[] attributesToReturn) {
            if (attributesToReturn == null)
                throw new ArgumentNullException("attributesToReturn");

            this.OuterNodeAttributes = new List<XAttribute>();
            this.ContentNodes = new List<IAttributeControl>(attributesToReturn);
            this.NodeName = "AttributesToReturn";
        }
    }
}