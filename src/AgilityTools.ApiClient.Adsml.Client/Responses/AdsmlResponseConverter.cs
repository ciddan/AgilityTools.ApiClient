using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public abstract class AdsmlResponseConverter<TOutput> : IResponseConverter<XElement, TOutput> where TOutput : AdsmlResult, new()
    {
        public virtual TOutput Convert(XElement source) {
            return new TOutput
                   {
                       Code = (string) source.Descendants("DeleteResponse").Single().Attribute("code"),
                       Description = (string) source.Descendants("DeleteResponse").Single().Attribute("description"),
                       Messages = source.Descendants("Message").Select(x => x.Value).ToList()
                   };
        }
    }
}