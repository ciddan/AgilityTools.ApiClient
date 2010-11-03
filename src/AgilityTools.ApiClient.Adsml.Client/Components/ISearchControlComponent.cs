using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public interface ISearchControlComponent
    {
        IList<XAttribute> OuterNodeAttributes { get; set; }
        XElement ToApiXml();
    }
}