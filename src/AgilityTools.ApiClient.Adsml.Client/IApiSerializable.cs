using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public interface IApiSerializable {
        XElement ToApiXml();
        void Validate();
    }
}