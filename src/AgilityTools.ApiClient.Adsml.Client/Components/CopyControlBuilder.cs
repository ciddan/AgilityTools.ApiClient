using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public class CopyControlBuilder : ICopyControlBuilder
    {
        internal ILookupControlBuilder LookupControlBuilder { get; set; }
        internal IList<ICopyControlFilter> RequestFilters { get; private set; }

        public CopyControlBuilder() {
            this.RequestFilters = new List<ICopyControlFilter>();
        }

        public ICopyControlConfigLookupControls CopyLocalAttributesFromSource() {
            this.RequestFilters.Add(Filter.CopyLocalAttributesFromSource());

            return this;
        }

        public ILookupControlBuilder ConfigureLookupControls() {
            this.LookupControlBuilder = new LookupControlBuilder();

            return this.LookupControlBuilder;
        }

        public CopyControl Build() {
            return new CopyControl(this.RequestFilters, this.LookupControlBuilder.Build());
        }
    }
}