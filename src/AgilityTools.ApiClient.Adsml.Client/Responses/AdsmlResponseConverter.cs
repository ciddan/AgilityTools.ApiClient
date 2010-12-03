using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public abstract class AdsmlResponseConverter<TOutput> : IResponseConverter<XElement, TOutput> where TOutput : AdsmlResult, new()
    {
        protected string ElementName { get; set; }

        public virtual TOutput Convert(XElement source) {

            if (string.IsNullOrEmpty(ElementName)) {
                throw new System.InvalidOperationException("ElementName must be set.");
            }

            return new TOutput
                   {
                       Code = (string) source.Descendants(this.ElementName).Single().Attribute("code"),
                       Description = (string) source.Descendants(this.ElementName).Single().Attribute("description"),
                       Messages = source.Descendants("Message").Select(x => x.Value).ToList()
                   };
        }
    }
}