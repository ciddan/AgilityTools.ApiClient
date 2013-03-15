using System;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
    public class LookupRequestBuilder : ILookupRequestBuilder
    {
        internal string Name { get; private set; }
        internal LookupControlBuilder LookupControlBuilder { get; private set; }

        public IConfigLookupControlsLookup ContextName(string name) {
            this.Name = name;

            if (string.IsNullOrEmpty(name)) {
                throw new InvalidOperationException("name cannot be null or empty");
            }

            return this;
        }

        public ILookupControlBuilder ConfigureLookupControls() {
            this.LookupControlBuilder = new LookupControlBuilder();

            return this.LookupControlBuilder;
        }

        public LookupRequest Build() {
            return this.LookupControlBuilder != null
                ? new LookupRequest(this.Name, this.LookupControlBuilder.Build()) 
                : new LookupRequest(this.Name);
        }
    }
}