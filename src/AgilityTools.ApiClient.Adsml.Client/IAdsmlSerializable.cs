using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public interface IAdsmlSerializable<out TResult> where TResult : XObject {
        TResult ToAdsml();
    }
}