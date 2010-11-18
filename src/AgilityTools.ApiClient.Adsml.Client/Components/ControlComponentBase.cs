using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public abstract class ControlComponentBase<TContentNode> : IControlComponent where TContentNode : IAdsmlSerializable<XObject>
    {
        protected string NodeName { get; set; }
        protected IList<TContentNode> ContentNodes { get; set; }
        public IList<XAttribute> OuterNodeAttributes { get; set; }

        public XElement ToAdsml() {
            return this.OuterNodeAttributes != null
                       ? new XElement(NodeName,
                                      this.OuterNodeAttributes.Select(attrs => attrs),
                                      ContentNodes.Select(cnode => cnode.ToAdsml()))
                       : new XElement(NodeName, 
                                      ContentNodes.Select(cnode => cnode.ToAdsml()));
        }
    }
}