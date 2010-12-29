using System;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public interface IApiClient : IDisposable
    {
        XElement SendApiRequest<TRequest>(TRequest request) where TRequest : class, IAdsmlSerializable<XElement>;
        void SendApiRequestAsync<TRequest>(TRequest request, Action<XElement> callback) where TRequest : class, IAdsmlSerializable<XElement>;
    }
}