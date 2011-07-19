using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    /// <summary>
    /// Abstract base class for all response-types returned by the Api.
    /// </summary>
    public abstract class AdsmlResult
    {
        public IEnumerable<string> Messages { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}