using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
  /// <summary>
  /// Abstract base implementation of <see cref="IControlComponent"/>. Used for more elaborate container types.
  /// </summary>
  public abstract class ControlComponentBase<TContentNode> : IControlComponent where TContentNode : IAdsmlSerializable<XObject>
  {
    protected string NodeName { get; set; }
    protected IList<TContentNode> ContentNodes { get; set; }
    public IList<XAttribute> OuterNodeAttributes { get; set; }

    /// <summary>
    /// Serializes the object to ADSML xml form.
    /// </summary>
    /// <returns><see cref="XElement"/></returns>
    public XElement ToAdsml() {
      return this.OuterNodeAttributes != null
        ? new XElement(
            NodeName,
            this.OuterNodeAttributes.Select(attrs => attrs),
            ContentNodes.Select(cnode => cnode.ToAdsml())
          )
        : new XElement(NodeName, ContentNodes.Select(cnode => cnode.ToAdsml()));
    }
  }
}