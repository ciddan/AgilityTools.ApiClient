using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    /// <summary>
    /// States that all request filters must be serializable into an <see cref="XAttribute"/> and adhere to the ADSML syntax.
    /// </summary>
    public interface IRequestFilter : IAdsmlSerializable<XAttribute> { }
}