using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class ReferenceControl : ISearchControlComponent
    {
        public IList<XAttribute> OuterNodeAttributes { get; set; }

        private readonly IEnumerable<IRequestFilter> _contentNodes;

        internal ReferenceControl(params IReferenceOptions[] referenceOptions) {
            if (referenceOptions == null)
                throw new ArgumentNullException("referenceOptions");

            _contentNodes = new List<IRequestFilter>(referenceOptions);
        }

        public XElement ToApiXml() {
            throw new NotImplementedException();
        }
    }
}