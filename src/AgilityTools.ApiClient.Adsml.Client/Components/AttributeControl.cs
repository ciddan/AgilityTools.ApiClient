using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class AttributeControl : ISearchControlComponent
    {
        public IList<XAttribute> OuterNodeAttributes { get; set; }

        private readonly IEnumerable<IAttributeControl> _contentNodes;

        internal AttributeControl(params IAttributeControl[] attributesToReturn) {
            if (attributesToReturn == null)
                throw new ArgumentNullException("attributesToReturn");

            _contentNodes = new List<IAttributeControl>(attributesToReturn);
        }

        public XElement ToAdsml() {
            return this.OuterNodeAttributes != null
                       ? new XElement("AttributesToReturn",
                                      this.OuterNodeAttributes.Select(attrs => attrs),
                                      _contentNodes.Select(cnode => cnode.ToAdsml()))
                       : new XElement("AttributesToReturn",
                                      _contentNodes.Select(cnode => cnode.ToAdsml()));
        }
    }
}