using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public interface IAdsmlAttribute : IAdsmlSerializable<XElement> {} //where T : XObject { }
}