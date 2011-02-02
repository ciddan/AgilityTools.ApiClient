using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public abstract class AdsmlResultResponseConverter<TOutput> : IResponseConverter<XElement, TOutput> where TOutput : AdsmlResult, new()
    {
        protected string ElementName { get; set; }

        protected virtual TOutput ConvertSingle(XElement source) {
            return new TOutput
                   {
                       Code = (string) source.Attribute("code"),
                       Description = (string) source.Attribute("description"),
                       Messages = source.Descendants("Message").Select(x => x.Value).ToList()
                   };
        }

        public virtual IEnumerable<TOutput> Convert(XElement source) {
            if (string.IsNullOrEmpty(ElementName)) {
                throw new InvalidOperationException("ElementName must be set.");
            }

            var results = source.Descendants().Where(d => d.Name.LocalName == this.ElementName);

            return results.Select(ConvertSingle);
        }
    }
}