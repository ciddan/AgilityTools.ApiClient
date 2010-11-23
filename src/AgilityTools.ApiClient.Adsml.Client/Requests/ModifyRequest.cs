using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Filters;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
    public class ModifyRequest : IAdsmlSerializable<XElement>
    {
        public LookupControl LookupControl { get; set; }
        public IEnumerable<IModifyRequestFilter> RequestFilters { get; set; }

        private readonly string _contextToModify;
        private readonly IList<ModificationItem> _modifications;

        public ModifyRequest(string contextToModify, IList<ModificationItem> modifications) {
            if (contextToModify == null) throw new ArgumentNullException("contextToModify");
            if (modifications == null) throw new ArgumentNullException("modifications");

            _contextToModify = contextToModify;
            _modifications = modifications;
        }

        public XElement ToAdsml() {
            this.Validate();

            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var request = new XElement("BatchRequest",
                                        new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                                        new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                                        new XElement("ModifyRequest",
                                                     new XAttribute("name", this._contextToModify), 
                                                     _modifications.Select(m => m.ToAdsml())));

            if (this.LookupControl != null) {
                request.Descendants("ModifyRequest").Single().AddFirst(this.LookupControl.ToAdsml());
            }

            if (this.RequestFilters != null)
                request.Descendants("ModifyRequest").Single().Add(this.RequestFilters.Select(rf => rf.ToAdsml()));

            return request;
        }

        private void Validate() {
            if (string.IsNullOrEmpty(this._contextToModify)) {
                throw new ApiSerializationValidationException("A context to modify must be specified.");
            }

            if(_modifications.Count() == 0) {
                throw new ApiSerializationValidationException("At least one ModificationItem must be specified.");
            }
        }
    }
}