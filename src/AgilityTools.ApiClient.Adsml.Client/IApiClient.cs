using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
  public interface IApiClient : IDisposable
  {
    /// <summary>
    /// Sends a request to the Agility API.
    /// </summary>
    /// <typeparam name="TRequest">The type of request to send.</typeparam>
    /// <param name="request">The request to send. Must be an instance of a class that implements <see cref="IAdsmlSerializable{XElement}"/>.</param>
    /// <returns>An <see cref="XElement"/> with the result of the request.</returns>
    /// <exception cref="AdsmlException">Thrown if the response contains an error.</exception>
    XElement SendApiRequest<TRequest>(TRequest request) where TRequest : class, IAdsmlSerializable<XElement>;

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
    IEnumerable<TResponse> SendApiRequest<TRequest, TResponse>(TRequest request, Func<XElement, IEnumerable<TResponse>> responseConverter)
      where TRequest : class, IAdsmlSerializable<XElement>;

    /// <summary>
    /// Sends a request to the Api server and then executes the callback.
    /// </summary>
    /// <typeparam name="TRequest">The type of request to send.</typeparam>
    /// <param name="request">Required. The request to send. Must be an instance of a class that implements <see cref="IAdsmlSerializable{XElement}"/>.</param>
    /// <param name="callback">Required.</param>
    /// <exception cref="AdsmlException">Thrown if the response contains an error.</exception>
    /// <exception cref="ArgumentNullException">Thrown if any of the required paramaters (<paramref name="request"/>, <paramref name="callback"/>) are null.</exception>
    void SendApiRequestAsync<TRequest>(TRequest request, Action<XElement> callback)
      where TRequest : class, IAdsmlSerializable<XElement>;

    Task<XElement> SendApiRequestAsync(string request);
    Task<IEnumerable<TResponse>> SendApiRequestAsync<TRequest, TResponse>(
      TRequest request, Func<XElement, IEnumerable<TResponse>> responseConverter
    ) where TRequest : class, IAdsmlSerializable<XElement>;
  }
}