using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Responses.Converters;
using AgilityTools.ApiClient.Adsml.Communication;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class ApiClient : IApiClient
    {
        private readonly IApiWebClient _webClient;
        private readonly string _adapiWsUrl;

        public ApiClient(IApiWebClient webClient, string adapiWsUrl = "http://penny:9080/Agility/Directory") {
            if (webClient == null) 
                throw new ArgumentNullException("webClient");

            _webClient = webClient;
            _adapiWsUrl = adapiWsUrl;
        }

        public XElement SendApiRequest<TRequest>(TRequest request) where TRequest : class, IAdsmlSerializable<XElement> {
            XElement result = SendRequest(request);

            return result;
        }

        public IEnumerable<TResponse> SendApiRequest<TRequest, TResponse>(TRequest request, Func<XElement, IEnumerable<TResponse>> responseConverter) 
            where TRequest : class, IAdsmlSerializable<XElement> {
            XElement result = SendRequest(request);

            return responseConverter.Invoke(result);
        }

        public void SendApiRequestAsync<TRequest>(TRequest request, Action<XElement> callback) where TRequest : class, IAdsmlSerializable<XElement> {
            if (request == null)
                throw new ArgumentNullException("request");

            byte[] q = BuildRequest(request);

            _webClient.UploadDataAsync(_adapiWsUrl, "POST", q, data => {
                                                                   var result =
                                                                       XElement.Parse(Encoding.UTF8.GetString(data));

                                                                   result.FirstNode.Remove();
                                                                   ValidateResponse(result);

                                                                   callback.Invoke(result);
                                                               });
        }

        private XElement SendRequest<TRequest>(TRequest request) where TRequest : class, IAdsmlSerializable<XElement> {
            if (request == null)
                throw new ArgumentNullException("request");

            byte[] q = BuildRequest(request);

            byte[] res = _webClient.UploadData(_adapiWsUrl, "POST", q);

            XElement result = XElement.Parse(Encoding.UTF8.GetString(res));

            // IBM WebSphere always returns an ErrorResponse first for some reason.
            // If a real error occurs, the response will contain two ErrorResponse nodes.
            result.FirstNode.Remove();

            ValidateResponse(result);

            return result;
        }

        private static void ValidateResponse(XElement result) {
            if (!result.Descendants().Any(n => n.Name.LocalName == "ErrorResponse")) {
                return;
            }
            
            var converter = new ErrorResponseConverter();
            var errors = converter.Convert(result);

            string errorMessages =
                errors.Aggregate(string.Empty, (current, error) => current + (error.ToString() + "\n")).Trim();

            throw new AdsmlException(
                string.Format("{0}\n{1}", "The request failed:", errorMessages),
                errors);
        }

        private static byte[] BuildRequest<TRequest>(TRequest request) where TRequest : class, IAdsmlSerializable<XElement> {
            var queryString = String.Format("xml=<?xml version=\"1.0\" encoding=\"utf-8\"?>{0}", request.ToAdsml());

            queryString = queryString.Replace("&amp;", "%26amp%3B");
            queryString = queryString.Replace("&quot;", "%26quot%3B");

            return Encoding.Default.GetBytes(queryString);
        }

        public void Dispose() {
            _webClient.Dispose();
        }
    }
}