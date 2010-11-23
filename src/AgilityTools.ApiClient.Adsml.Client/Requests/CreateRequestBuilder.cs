using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using AgilityTools.ApiClient.Adsml.Client.Filters;
using AgilityTools.ApiClient.Adsml.Client.FluentSyntax;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
    public class CreateRequestBuilder : ICreateRequestBuilder
    {
        internal string ContextName { get; private set; }
        internal string ObjectType { get; private set; }
        internal IAdsmlAttribute<XElement>[] Attributes { get; private set; }
        internal LookupControlBuilder LookupControlBuilder { get; private set; }
        internal IList<ICreateRequestFilter> RequestFilters { get; private set; }

        public CreateRequestBuilder() {
            this.RequestFilters = new List<ICreateRequestFilter>();
        }

        public IOTTCreateRNAttributesIFOErrorIATSet NewContextName(string contextName) {
            this.ContextName = contextName;

            return this; 
        }

        public IRNAttributesFOErrorIATSet ObjectTypeToCreate(string objectType) {
            this.ObjectType = objectType;
            
            return this;
        }

        public IFOErrorIATSet ReturnNoAttributes() {
            this.RequestFilters.Add(Filter.OmitStructureAttributes());

            return this;
        }

        public IAttributesToSet FailOnError() {
            this.RequestFilters.Add(Filter.FailOnError());

            return this;
        }

        public IConfigLookupControls AttributesToSet(IEnumerable<IAdsmlAttribute<XElement>> structureAttributes) {
            this.Attributes = structureAttributes.ToArray();

            return this;
        }

        public IConfigLookupControls AttributesToSet(params IAdsmlAttribute<XElement>[] structureAttributes) {
            this.Attributes = structureAttributes;

            return this;
        }

        public IConfigLookupControls AttributesToSet(Func<IList<IAdsmlAttribute<XElement>>> attributeFactory) {
            this.Attributes = attributeFactory.Invoke().ToArray();
            
            return this;
        }

        public ILookupControlBuilder ConfigureLookupControls() {
            this.LookupControlBuilder = new LookupControlBuilder();

            return this.LookupControlBuilder;
        }

        public CreateRequest Build() {
            var createRequest = new CreateRequest(this.ObjectType, this.ContextName, this.Attributes);

            if (this.LookupControlBuilder != null)
                createRequest.LookupControl = this.LookupControlBuilder.Build();

            if (this.RequestFilters.Count() >= 1)
                createRequest.RequestFilters = this.RequestFilters;

            return createRequest;
        }
    }
}