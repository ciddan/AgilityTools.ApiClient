using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// A builder-class that generates a <see cref="CopyControl"/>.
    /// </summary>
    public class CopyControlBuilder : ICopyControlBuilder
    {
        internal ILookupControlBuilder LookupControlBuilder { get; set; }
        internal IList<ICopyControlFilter> RequestFilters { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public CopyControlBuilder() {
            this.RequestFilters = new List<ICopyControlFilter>();
        }

        /// <summary>
        /// Adds a filter to the <see cref="CopyControl"/> that indicates that local attributes should be copied from the global source.
        /// </summary>
        /// <returns>A <see cref="ICopyControlConfigLookupControls"/> representation of the object instance.</returns>
        public ICopyControlConfigLookupControls CopyLocalAttributesFromSource() {
            this.RequestFilters.Add(Filter.CopyLocalAttributesFromSource());

            return this;
        }

        /// <summary>
        /// Adds a lookup control to the resulting <see cref="CopyControl"/>. 
        /// </summary>
        /// <returns><see cref="ILookupControlBuilder"/></returns>
        public ILookupControlBuilder ConfigureLookupControls() {
            this.LookupControlBuilder = new LookupControlBuilder();

            return this.LookupControlBuilder;
        }

        /// <summary>
        /// Builds a <see cref="CopyControl"/> using all specified parameters.
        /// </summary>
        /// <returns><see cref="CopyControl"/>.</returns>
        public CopyControl Build() {
            return this.LookupControlBuilder != null ? 
                new CopyControl(this.RequestFilters, this.LookupControlBuilder.Build())
                : new CopyControl(this.RequestFilters);
        }
    }
}