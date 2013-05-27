using System;
using System.Collections.Generic;
using System.IO;
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
    private readonly string _userName;
    private readonly string _password;
    private readonly string _validationDocument;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="webClient">Required. Used to send data to the API.</param>
    /// <param name="adapiWsUrl">Required. A url to an Agility Directory Services endpoint. Example: <example>http://192.168.0.100:9080/Agility/Directory</example>.</param>
    /// <param name="userName">Required. Username for Agility auth.</param>
    /// <param name="password">Required. Password for Agility auth.</param>
    /// <param name="validationDocument">Optional. Relative or full path to an Agility ADMSL API definition file. Used for query validation. Defaults to <example>adsml.xsd</example>.</param>
    public ApiClient(IApiWebClient webClient, string adapiWsUrl, string userName, string password, string validationDocument = "adsml.xsd") {
      if (webClient == null) throw new ArgumentNullException("webClient");

      _webClient = webClient;
      _adapiWsUrl = adapiWsUrl;
      _userName = userName;
      _password = password;

      if (string.IsNullOrEmpty(adapiWsUrl)) {
        throw new ArgumentNullException("adapiWsUrl");
      }

      if (string.IsNullOrEmpty(userName)) {
        throw new ArgumentNullException("userName");
      }

      if (string.IsNullOrEmpty(password)) {
        throw new ArgumentNullException("password");
      }

      if (!File.Exists(validationDocument)) {
        throw new FileNotFoundException("API definition file not found.", "adsml.xsd");
      }

      _validationDocument = validationDocument;
    }

    /// <summary>
    /// Sends a request to the Agility API.
    /// </summary>
    /// <typeparam name="TRequest">The type of request to send.</typeparam>
    /// <param name="request">The request to send. Must be an instance of a class that implements <see cref="IAdsmlSerializable{XElement}"/>.</param>
    /// <returns>An <see cref="XElement"/> with the result of the request.</returns>
    /// <exception cref="AdsmlException">Thrown if the response contains an error.</exception>
    public XElement SendApiRequest<TRequest>(TRequest request) where TRequest : class, IAdsmlSerializable<XElement> {
      return SendRequest(request);
    }

    /// <summary>
    /// Sends a request to the Agility API.
    /// </summary>
    /// <param name="request">The request to send.</param>
    /// <returns>An <see cref="XElement"/> with the result of the request.</returns>
    /// <exception cref="AdsmlException">Thrown if the response contains an error.</exception>
    public XElement SendApiRequest(string request) {
      return SendRequest(request);
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
    public IEnumerable<TResponse> SendApiRequest<TRequest, TResponse>(TRequest request, Func<XElement, IEnumerable<TResponse>> responseConverter) 
      where TRequest : class, IAdsmlSerializable<XElement> {
      if (request == null) throw new ArgumentNullException("request");
      if (responseConverter == null) throw new ArgumentNullException("responseConverter");

      XElement result = SendRequest(request);

      return responseConverter.Invoke(result);
    }

    /// <summary>
    /// Sends a request to the Agility API.
    /// </summary>
    /// <typeparam name="TResponse">The type that the response gets converted to by the converter.</typeparam>
    /// <param name="request">Required. The request to send.</param>
    /// <param name="responseConverter">Required. A converter function used to convert the result from an XElement into the desired class.</param>
    /// <returns>An <see cref="IEnumerable{TResult}"/>.</returns>
    /// <exception cref="AdsmlException">Thrown if the response contains an error.</exception>
    /// <exception cref="ArgumentNullException">Thrown if any of the required paramaters (<paramref name="request"/>, <paramref name="responseConverter"/>) are null.</exception>
    public IEnumerable<TResponse> SendApiRequest<TResponse>(string request, Func<XElement, IEnumerable<TResponse>> responseConverter) {
      if (request == null) throw new ArgumentNullException("request");
      if (responseConverter == null) throw new ArgumentNullException("responseConverter");

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
    public void SendApiRequestAsync<TRequest>(TRequest request, Action<XElement> callback) where TRequest : class, IAdsmlSerializable<XElement> {
      if (request == null)
        throw new ArgumentNullException("request");

      if (callback == null) {
        throw new ArgumentNullException("callback");
      }

      string req = BuildRequest(request);
      _webClient.UploadStringAsync(
        _adapiWsUrl, req, data => {
        var result = XElement.Parse(data);
        ValidateResponse(result);

        callback.Invoke(result);
      }
      );
    }

    /// <summary>
    /// Private function that sends requests to the API.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="request">Required. The request to send.</param>
    /// <returns>An <see cref="XElement"/> that contains the response.</returns>
    private XElement SendRequest<TRequest>(TRequest request) where TRequest : class, IAdsmlSerializable<XElement> {
      if (request == null) throw new ArgumentNullException("request");

      string req = BuildRequest(request);
      string response = _webClient.UploadString(_adapiWsUrl, req);

      XElement result = XElement.Parse(response);
      ValidateResponse(result);

      return result;
    }

    /// <summary>
    /// Private function that sends requests to the API.
    /// </summary>
    /// <param name="request">Required. The request to send.</param>
    /// <returns>An <see cref="XElement"/> that contains the response.</returns>
    private XElement SendRequest(string request) {
      if (request == null) throw new ArgumentNullException("request");

      string req = BuildRequest(request);
      string response = _webClient.UploadString(_adapiWsUrl, req);

      XElement result = XElement.Parse(response);
      ValidateResponse(result);

      return result;
    }

    /// <summary>
    /// Checks to see whether the Api-response contains any errors. If so, throws an <see cref="AdsmlException"/> containing the error details.
    /// </summary>
    /// <param name="result">The response to check for errors.</param>
    private void ValidateResponse(XElement result) {
      if (result.Descendants().All(n => n.Name.LocalName != "ErrorResponse")) {
        return;
      }

      var converter = new ErrorResponseConverter(_validationDocument);
      var errors = converter.Convert(result).ToList();

      string errorMessages =
        errors.Aggregate(string.Empty, (current, error) => current + (error.ToString() + "\n")).Trim();

      throw new AdsmlException(
        string.Format("{0}\n{1}", "The request failed:", errorMessages),
        errors
        );
    }

    /// <summary>
    /// Converts the request from a <see cref="IAdsmlSerializable{TResult}"/> of <see cref="XElement"/> into a url-encoded <see cref="string"/>. 
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <param name="request">The request to encode.</param>
    /// <returns>A url-encoded string representation of the request.</returns>
    private string BuildRequest<TRequest>(TRequest request)
      where TRequest : class, IAdsmlSerializable<XElement> {
      var queryString = request.ToAdsml().ToString();

      queryString = System.Web.HttpUtility.UrlEncode(queryString, Encoding.UTF8);
      queryString = string.Format("xml={0}&user={1}&password={2}", queryString, _userName, PasswordEncoder.EncodePassword(_password));

      return queryString;
    }

    /// <summary>
    /// Converts the request from a <see cref="IAdsmlSerializable{TResult}"/> of <see cref="XElement"/> into a url-encoded <see cref="string"/>. 
    /// </summary>
    /// <param name="request">The request to encode.</param>
    /// <returns>A url-encoded string representation of the request.</returns>
    private string BuildRequest(string request) {
      var queryString = request;

      queryString = System.Web.HttpUtility.UrlEncode(queryString, Encoding.UTF8);
      queryString = string.Format("xml={0}&user={1}&password={2}", queryString, _userName, PasswordEncoder.EncodePassword(_password));

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