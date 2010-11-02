using System;

namespace AgilityTools.ApiClient.Adsml.Communication
{
    public interface IApiWebClient : IDisposable
    {
        byte[] UploadData(string url, string method, byte[] data);
    }
}