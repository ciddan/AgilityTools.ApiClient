using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class ReferenceControl : ISearchControlComponent
    {
        public IList<XAttribute> OuterNodeAttributes { get; set; }

        private readonly IEnumerable<IReferenceOptions> _referenceOptions;

        internal ReferenceControl(params IReferenceOptions[] referenceOptions) {
            if (referenceOptions == null)
                throw new ArgumentNullException("referenceOptions");

            _referenceOptions = new List<IReferenceOptions>(referenceOptions);
        }

        public XElement ToApiXml() {
            return this.OuterNodeAttributes != null
                       ? new XElement("ReferenceControls",
                                      this.OuterNodeAttributes.Select(attrs => attrs),
                                      _referenceOptions.Select(cnode => cnode.ToAdsml()))
                       : new XElement("ReferenceControls",
                                      _referenceOptions.Select(cnode => cnode.ToAdsml()));
        }
    }
}