using System;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Responses.Converters;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public interface IApiClient : IDisposable
    {
        XElement SendApiRequest<TRequest>(TRequest request) where TRequest : class, IAdsmlSerializable<XElement>;
        void SendApiRequestAsync<TRequest>(TRequest request, Action<XElement> callback) where TRequest : class, IAdsmlSerializable<XElement>;

        TOutput SendApiRequest<TRequest, TOutput>(TRequest request, IResponseConverter<XElement, TOutput> converter)
            where TRequest : class, IAdsmlSerializable<XElement>
            where TOutput : class;

        void SendApiRequestAsync<TRequest, TOutput>(TRequest request, IResponseConverter<XElement, TOutput> converter, Action<TOutput> callback) 
            where TRequest : class, IAdsmlSerializable<XElement>
            where TOutput : class;
    }
}