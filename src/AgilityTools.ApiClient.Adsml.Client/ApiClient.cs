using System;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Communication;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class ApiClient : IApiClient
    {
        private readonly IApiWebClient _webClient;
        private readonly string _adapiWsUrl;

        public ApiClient(IApiWebClient webClient, string adapiWsUrl = "http://penny:9080/Agility/Directory")
        {
            if (webClient == null) 
                throw new ArgumentNullException("webClient");

            _webClient = webClient;
            _adapiWsUrl = adapiWsUrl;
        }

        public XElement SendApiRequest<TRequest>(TRequest request) where TRequest : IApiSerializable
        {
            return new XElement("result");
        }

        public void Dispose()
        {
            _webClient.Dispose();
        }
    }
}