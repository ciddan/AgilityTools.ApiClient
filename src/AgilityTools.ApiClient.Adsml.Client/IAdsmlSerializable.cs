using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public interface IAdsmlSerializable {
        XElement ToApiXml();
        void Validate();
    }
}