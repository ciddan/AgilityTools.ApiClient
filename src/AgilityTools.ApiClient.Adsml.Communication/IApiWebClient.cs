using System;

namespace AgilityTools.ApiClient.Adsml.Communication
{
    public interface IApiWebClient : IDisposable
    {
        string UploadString(string url, string request);
        void UploadStringAsync(string url, string data, Action<string> callback);
    }
}