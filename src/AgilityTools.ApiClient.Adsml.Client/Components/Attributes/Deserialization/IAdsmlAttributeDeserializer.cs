using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public interface IAdsmlAttributeDeserializer
    {
        IAdsmlAttribute Deserialize(XElement element);
    }
}