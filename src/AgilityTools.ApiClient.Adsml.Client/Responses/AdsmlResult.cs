using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public abstract class AdsmlResult
    {
        public IEnumerable<string> Messages { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}