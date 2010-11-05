using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class AttributeSearchControl : ISearchControlComponent
    {
        public IList<XAttribute> OuterNodeAttributes { get; set; }

        private readonly IEnumerable<IAttributeControl> _contentNodes;

        internal AttributeSearchControl(params IAttributeControl[] attributesToReturn) {
            if (attributesToReturn == null)
                throw new ArgumentNullException("attributesToReturn");

            _contentNodes = new List<IAttributeControl>(attributesToReturn);
        }

        public XElement ToApiXml() {
            return this.OuterNodeAttributes != null
                       ? new XElement("AttributesToReturn",
                                      this.OuterNodeAttributes.Select(attrs => attrs),
                                      _contentNodes.Select(cnode => cnode.ToApiXml()))
                       : new XElement("AttributesToReturn",
                                      _contentNodes.Select(cnode => cnode.ToApiXml()));

        }
    }
}