using System;

namespace AgilityTools.ApiClient.Adsml.Communication
{
    /// <summary>
    /// Provides functionality for uploading requests to the Agility API.
    /// </summary>
    public interface IApiWebClient : IDisposable
    {
        ///<summary>
        ///Uploads a request to the Agility API and returns the response.
        ///</summary>
        ///<param name="url">Required. The URL of the Agility API endpoint.</param>
        ///<param name="request">Required. The request to send to the Agility API.</param>
        ///<exception cref="ArgumentNullException">Thrown if any of the required parameters (<paramref name="url"/>, <paramref name="request" />) are null.</exception>
        ///<exception cref="InvalidOperationException">Thrown if any of the required parameters (<paramref name="url"/>, <paramref name="request" />) are empty.</exception>
        ///<returns>A <see cref="string"/> containing the result.</returns>
        string UploadString(string url, string request);

        ///<summary>
        ///Uploads a request to the Agility API and returns the response. Executes the callback When the result is returned.
        ///</summary>
        ///<param name="url">Required. The URL of the Agility API endpoint.</param>
        ///<param name="request">Required. The request to send to the Agility API.</param>
        ///<param name="callback">Required. The request to send to the Agility API.</param>
        ///<exception cref="ArgumentNullException">Thrown if any of the required parameters (<paramref name="url" />, <paramref name="request" />, <paramref name="callback" />) are null.</exception>
        ///<exception cref="InvalidOperationException">Thrown if any of the required parameters (<paramref name="url" />, <paramref name="request" />) are empty.</exception>
        void UploadStringAsync(string url, string request, Action<string> callback);
    }
}