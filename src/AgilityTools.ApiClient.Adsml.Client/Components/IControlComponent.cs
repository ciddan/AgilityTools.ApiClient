using System.Xml.Linq;
using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public interface IControlComponent
    {
        IList<XAttribute> OuterNodeAttributes { get; set; }
        XElement ToApiXml();
    }
}