using System;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public interface IApiClient : IDisposable
    {
        XElement SendApiRequest<TRequest>(TRequest request) where TRequest : IAdsmlSerializable<XElement>;
    }
}