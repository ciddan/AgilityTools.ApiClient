using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public interface IRequestFilter
    {
        XAttribute ToAdsml();
    }
}