using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public interface IAdsmlAttribute : IAdsmlSerializable<XElement>
    {
        string GetName();
        IEnumerable<AttributeValue> GetValues();
    }
}