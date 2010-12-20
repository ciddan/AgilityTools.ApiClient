using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components.Attributes
{
    public interface IAdsmlAttribute : IAdsmlSerializable<XElement> {} //where T : XObject { }
}