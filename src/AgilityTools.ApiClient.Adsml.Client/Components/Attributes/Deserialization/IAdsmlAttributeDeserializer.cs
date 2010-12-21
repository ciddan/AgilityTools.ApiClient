using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components.Attributes.Deserialization
{
    public interface IAdsmlAttributeDeserializer
    {
        IAdsmlAttribute Deserialize(XElement element);
    }
}