using System;
using System.Net;
using System.Text;

namespace AgilityTools.ApiClient.Adsml.Communication
{
    public class ApiWebClient : IApiWebClient
    {
        private readonly WebClient _webClient;

        public ApiWebClient() {
            _webClient = new WebClient {Encoding = Encoding.UTF8};
        }

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

            _webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            return _webClient.UploadString(url, request);
        }

        public void UploadStringAsync(string url, string data, Action<string> callback) {
            if (url == null) {
                throw new ArgumentNullException("url");
            }

            if (data == null) {
                throw new ArgumentNullException("data");
            }

            if (callback == null) {
                throw new ArgumentNullException("callback");
            }

            if (string.IsNullOrEmpty(url)) {
                throw new InvalidOperationException("Url cannot be empty.");
            }

            var uri = new Uri(url, UriKind.Absolute);

            _webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            _webClient.UploadStringCompleted += (sender, args) => callback.Invoke(args.Result);
            _webClient.UploadStringAsync(uri, data);
        }

        public void Dispose() {
            _webClient.Dispose();
        }
    }
}