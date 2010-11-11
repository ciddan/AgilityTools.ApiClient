using System.Xml.Linq;
using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public interface IControlComponent : IAdsmlSerializable<XElement>
    {
        IList<XAttribute> OuterNodeAttributes { get; set; }
    }
}