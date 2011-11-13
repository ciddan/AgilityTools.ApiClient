using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Responses;
using AgilityTools.ApiClient.Adsml.Communication;

namespace AgilityTools.ApiClient.Adsml.Client
{
    /// <summary>
    /// Implementation of IApiClient. Provides functionality for sending requests to the Agility API.
    /// </summary>
    public class ApiClient : IApiClient
    {
        private readonly IApiWebClient _webClient;
        private readonly string _adapiWsUrl;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="webClient">Required. Used to send data to the API.</param>
        /// <param name="adapiWsUrl">Optional. A url to an Agility Directory Services endpoint. Defaults to <example>http://penny:9080/Agility/Directory</example></param>
        public ApiClient(IApiWebClient webClient, string adapiWsUrl = "http://penny:9080/Agility/Directory") {
            if (webClient == null)
                throw new ArgumentNullException("webClient");

            _webClient = webClient;
            _adapiWsUrl = adapiWsUrl;
        }

        /// <summary>
        /// Sends a request to the Agility API.
        /// </summary>
        /// <typeparam name="TRequest">The type of request to send.</typeparam>
        /// <param name="request">The request to send. Must be an instance of a class that implements <see cref="IAdsmlSerializable{XElement}"/>.</param>
        /// <returns>An <see cref="XElement"/> with the result of the request.</returns>
        /// <exception cref="AdsmlException">Thrown if the response contains an error.</exception>
        public XElement SendApiRequest<TRequest>(TRequest request) where TRequest : class, IAdsmlSerializable<XElement> {
            XElement result = SendRequest(request);

            return result;
        }

        /// <summary>
        /// Sends a request to the Agility API.
        /// </summary>
        /// <typeparam name="TRequest">The type of request to send.</typeparam>
        /// <typeparam name="TResponse">The type that the response gets converted to by the converter.</typeparam>
        /// <param name="request">Required. The request to send. Must be an instance of a class that implements <see cref="IAdsmlSerializable{XElement}"/>.</param>
        /// <param name="responseConverter">Required. A converter function used to convert the result from an XElement into the desired class.</param>
        /// <returns>An <see cref="IEnumerable{TResult}"/>.</returns>
        /// <exception cref="AdsmlException">Thrown if the response contains an error.</exception>
        /// <exception cref="ArgumentNullException">Thrown if any of the required paramaters (<paramref name="request"/>, <paramref name="responseConverter"/>) are null.</exception>
        public IEnumerable<TResponse> SendApiRequest<TRequest, TResponse>(TRequest request,
                                                                          Func<XElement, IEnumerable<TResponse>>
                                                                              responseConverter)
            where TRequest : class, IAdsmlSerializable<XElement> {
            if (request == null) {
                throw new ArgumentNullException("request");
            }

            if (responseConverter == null) {
                throw new ArgumentNullException("responseConverter");
            }

            XElement result = SendRequest(request);

            return responseConverter.Invoke(result);
        }

        /// <summary>
        /// Sends a request to the Api server and then executes the callback.
        /// </summary>
        /// <typeparam name="TRequest">The type of request to send.</typeparam>
        /// <param name="request">Required. The request to send. Must be an instance of a class that implements <see cref="IAdsmlSerializable{XElement}"/>.</param>
        /// <param name="callback">Required.</param>
        /// <exception cref="AdsmlException">Thrown if the response contains an error.</exception>
        /// <exception cref="ArgumentNullException">Thrown if any of the required paramaters (<paramref name="request"/>, <paramref name="callback"/>) are null.</exception>
        public void SendApiRequestAsync<TRequest>(TRequest request, Action<XElement> callback)
            where TRequest : class, IAdsmlSerializable<XElement> {
            if (request == null)
                throw new ArgumentNullException("request");

            if (callback == null) {
                throw new ArgumentNullException("callback");
            }

            string req = BuildRequest(request);

            _webClient.UploadStringAsync(_adapiWsUrl, req, data => {
                                                               var result = XElement.Parse(data);

                                                               result.FirstNode.Remove();
                                                               ValidateResponse(result);

                                                               callback.Invoke(result);
                                                           });
        }

        /// <summary>
        /// Private function that sends requests to the API.
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request">Required. The request to send.</param>
        /// <returns>An <see cref="XElement"/> that contains the response.</returns>
        private XElement SendRequest<TRequest>(TRequest request) where TRequest : class, IAdsmlSerializable<XElement> {
            if (request == null)
                throw new ArgumentNullException("request");

            string req = BuildRequest(request);

            string response = _webClient.UploadString(_adapiWsUrl, req);

            XElement result = XElement.Parse(response);

            // IBM WebSphere always returns an ErrorResponse first for some reason.
            // If a real error occurs, the response will contain two ErrorResponse nodes.
            result.FirstNode.Remove();

            ValidateResponse(result);

            return result;
        }

        /// <summary>
        /// Checks to see whether the Api-response contains any errors. If so, throws an <see cref="AdsmlException"/> containing the error details.
        /// </summary>
        /// <param name="result">The response to check for errors.</param>
        private static void ValidateResponse(XElement result) {
            if (!result.Descendants().Any(n => n.Name.LocalName == "ErrorResponse")) {
                return;
            }

            var converter = new ErrorResponseConverter();
            var errors = converter.Convert(result).ToList();

            string errorMessages =
                errors.Aggregate(string.Empty, (current, error) => current + (error.ToString() + "\n")).Trim();

            throw new AdsmlException(
                string.Format("{0}\n{1}", "The request failed:", errorMessages),
                errors);
        }

        /// <summary>
        /// Converts the request from a <see cref="IAdsmlSerializable{TResult}"/> of <see cref="XElement"/> into a url-encoded <see cref="string"/>. 
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="request">The request to encode.</param>
        /// <returns>A url-encoded string representation of the request.</returns>
        private static string BuildRequest<TRequest>(TRequest request)
            where TRequest : class, IAdsmlSerializable<XElement> {
            var queryString = request.ToAdsml().ToString();

            queryString = System.Web.HttpUtility.UrlEncode(queryString, Encoding.UTF8);
            queryString = "xml=" + queryString;

            return queryString;
        }

        /// <summary>
        /// Disposes of the <see cref="IApiWebClient"/>.
        /// </summary>
        public void Dispose() {
            _webClient.Dispose();
        }
    }
}