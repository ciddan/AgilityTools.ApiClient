using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class LanguageControl : ISearchControlComponent
    {
        public IList<XAttribute> OuterNodeAttributes { get; set; }

        private readonly IEnumerable<IReturnedLanguageControl> _contentNodes;

        internal LanguageControl(params IReturnedLanguageControl[] languagesToReturn) {
            if (languagesToReturn == null)
                throw new ArgumentNullException("languagesToReturn");

            _contentNodes = new List<IReturnedLanguageControl>(languagesToReturn);
        }

        public XElement ToAdsml() {
            return this.OuterNodeAttributes != null
                       ? new XElement("LanguagesToReturn",
                                      this.OuterNodeAttributes.Select(attrs => attrs),
                                      _contentNodes.Select(cnode => cnode.ToAdsml()))
                       : new XElement("LanguagesToReturn",
                                      _contentNodes.Select(cnode => cnode.ToAdsml()));
        }
    }
}