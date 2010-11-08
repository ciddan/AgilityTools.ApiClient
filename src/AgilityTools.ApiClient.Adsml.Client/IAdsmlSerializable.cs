using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public interface IAdsmlSerializable {
        XElement ToAdsml();
        void Validate();
    }
}