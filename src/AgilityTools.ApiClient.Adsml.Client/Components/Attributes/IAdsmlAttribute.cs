using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components.Attributes
{
    public interface IAdsmlAttribute<out T> : IAdsmlSerializable<T> where T : XObject { }
}