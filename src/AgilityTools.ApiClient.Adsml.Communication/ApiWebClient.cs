using System;
using System.Net;
using System.Text;

namespace AgilityTools.ApiClient.Adsml.Communication
{
    ///<summary>
    ///Implementation of IApiWebClient. Provides functionality for uploading requests to the Agility API.
    ///</summary>
    public class ApiWebClient : IApiWebClient
    {
        private readonly WebClient _webClient;

        ///<summary>
        ///Constructor.
        ///</summary>
        public ApiWebClient() {
            _webClient = new WebClient {Encoding = Encoding.UTF8};
        }

        ///<summary>
        ///Uploads a request to the Agility API and returns the response.
        ///</summary>
        ///<param name="url">Required. The URL of the Agility API endpoint.</param>
        ///<param name="request">Required. The request to send to the Agility API.</param>
        ///<exception cref="ArgumentNullException">Thrown if any of the required parameters (<paramref name="url"/>, <paramref name="request" />) are null.</exception>
        ///<exception cref="InvalidOperationException">Thrown if any of the required parameters (<paramref name="url"/>, <paramref name="request" />) are empty.</exception>
        ///<returns>A <see cref="string"/> containing the result.</returns>
        public string UploadString(string url, string request) {
            if (url == null) {
                throw new ArgumentNullException("url");
            }

            if (request == null) {
                throw new ArgumentNullException("request");
            }

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException("Url must be provided.");

            if (string.IsNullOrEmpty(request))
                throw new InvalidOperationException("A request must be provided.");

            _webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");

            return _webClient.UploadString(url, request);
        }

        ///<summary>
        ///Uploads a request to the Agility API and returns the response. Executes the callback When the result is returned.
        ///</summary>
        ///<param name="url">Required. The URL of the Agility API endpoint.</param>
        ///<param name="request">Required. The request to send to the Agility API.</param>
        ///<param name="callback">Required. The request to send to the Agility API.</param>
        ///<exception cref="ArgumentNullException">Thrown if any of the required parameters (<paramref name="url" />, <paramref name="request" />, <paramref name="callback" />) are null.</exception>
        ///<exception cref="InvalidOperationException">Thrown if any of the required parameters (<paramref name="url" />, <paramref name="request" />) are empty.</exception>
        public void UploadStringAsync(string url, string request, Action<string> callback) {
            if (url == null) {
                throw new ArgumentNullException("url");
            }

            if (request == null) {
                throw new ArgumentNullException("request");
            }

            if (callback == null) {
                throw new ArgumentNullException("callback");
            }

            if (string.IsNullOrEmpty(url)) {
                throw new InvalidOperationException("Url cannot be empty.");
            }

            var uri = new Uri(url, UriKind.Absolute);

            _webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");

            _webClient.UploadStringCompleted += (sender, args) => callback.Invoke(args.Result);
            _webClient.UploadStringAsync(uri, request);
        }

        public void Dispose() {
            _webClient.Dispose();
        }
    }
}