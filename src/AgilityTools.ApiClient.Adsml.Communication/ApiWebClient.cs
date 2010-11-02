using System;
using System.Net;
using System.Text;

namespace AgilityTools.ApiClient.Adsml.Communication
{
    public class ApiWebClient : IApiWebClient
    {
        private readonly WebClient _webClient;

        public ApiWebClient()
        {
            _webClient = new WebClient {Encoding = Encoding.UTF8};
        }

        public byte[] UploadData(string url, string method, byte[] data)
        {
            if (data == null) 
                throw new ArgumentNullException("data");

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException("Url must be provided.");

            if (string.IsNullOrEmpty(method))
                throw new InvalidOperationException("A method must be provided.");

            _webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            return _webClient.UploadData(url, method, data);
        }

        public void Dispose()
        {
            _webClient.Dispose();
        }
    }
}