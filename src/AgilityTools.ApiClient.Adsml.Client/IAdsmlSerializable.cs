using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    /// <summary>
    /// Interface used to mark adsml request objects as convertiable to xml (or adsml for Agility Directory Services Markup Language).
    /// </summary>
    /// <typeparam name="TResult">The type of the resulting object. The resulting object's type must be or derive from <see cref="XObject"/>.</typeparam>
    public interface IAdsmlSerializable<out TResult> where TResult : XObject {
        TResult ToAdsml();
    }
}