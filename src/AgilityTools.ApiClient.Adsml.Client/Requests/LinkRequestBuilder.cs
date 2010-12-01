using System.Collections.Generic;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Filters;
using AgilityTools.ApiClient.Adsml.Client.Helpers;
using AgilityTools.ApiClient.Adsml.Client.FluentSyntax;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
    public class LinkRequestBuilder : ILinkRequestBuilder
    {
        internal IList<ILinkRequestFilter> RequestFilters { get; private set; }
        internal CopyControlBuilder CopyControlBuilder { get; private set; }
        internal string Source { get; private set; }
        internal string Target { get; private set; }

        public LinkRequestBuilder() {
            this.RequestFilters = new List<ILinkRequestFilter>();
        }

        public ILinkRequestTargetPathReturnNoAttributesConfigureCopyControls SourceContext(string sourcePath) {
            this.Source = sourcePath;

            return this;
        }

        public ILinkRequestReturnNoAttributesConfigureCopyControls TargetPath(string targetPath) {
            this.Target = targetPath;

            return this;
        }

        public ILinkRequestConfigureCopyControls ReturnNoAttributes() {
            this.RequestFilters.Add(Filter.ReturnNoAttributes());

            return this;
        }

        public ICopyControlBuilder ConfigureCopyControls() {
            this.CopyControlBuilder = new CopyControlBuilder();

            return this.CopyControlBuilder;
        }

        public LinkRequest Build() {
            this.Validate();

            var linkRequest = new LinkRequest(this.Source, this.Target)
                              {
                                  RequestFilters = new List<ILinkRequestFilter>(this.RequestFilters)
                              };

            if (this.CopyControlBuilder != null)
                linkRequest.CopyControl = this.CopyControlBuilder.Build();

            return linkRequest;
        }

        private void Validate() {
            if (Source.IsNullOrEmpty()) {
                throw new ApiSerializationValidationException("A Source context must be specified.");
            }

            if (Target.IsNullOrEmpty()) {
                throw new ApiSerializationValidationException("A Target path must be specified.");
            }
        }
    }
}