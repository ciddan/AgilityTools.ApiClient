using System;
using System.Collections.Generic;
using System.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
    public class CreateRequestBuilder : ICreateRequestBuilder
    {
        internal string ParentPath { get; private set; }
        internal string ContextName { get; private set; }
        internal string ObjectType { get; private set; }
        internal IAdsmlAttribute[] Attributes { get; private set; }
        internal LookupControlBuilder LookupControlBuilder { get; private set; }
        internal IList<ICreateRequestFilter> RequestFilters { get; private set; }

        public CreateRequestBuilder() {
            this.RequestFilters = new List<ICreateRequestFilter>();
        }

        public INewContextNameObjectTypeToCreateReturnNoAttributesFailOnErrorAttributesToSet ParentIdPath(string parentIdPath) {
            this.ParentPath = parentIdPath;

            return this;
        }

        public IObjectTypeToCreateReturnNoAttributesFailOnErrorAttributesToSet NewContextName(string contextName) {
            this.ContextName = contextName;

            return this; 
        }

        public IReturnNoAttributesFailOnErrorAttributesToSet ObjectTypeToCreate(string objectType) {
            this.ObjectType = objectType;
            
            return this;
        }

        public IFailOnErrorAttributesToSet ReturnNoAttributes() {
            this.RequestFilters.Add(Filter.ReturnNoAttributes());

            return this;
        }

        public IAttributesToSet FailOnError() {
            this.RequestFilters.Add(Filter.FailOnError());

            return this;
        }

        public IConfigLookupControls AttributesToSet(IEnumerable<IAdsmlAttribute> structureAttributes) {
            this.Attributes = structureAttributes.ToArray();

            return this;
        }

        public IConfigLookupControls AttributesToSet(params IAdsmlAttribute[] structureAttributes) {
            this.Attributes = structureAttributes;

            return this;
        }

        public IConfigLookupControls AttributesToSet(Func<IList<IAdsmlAttribute>> attributeFactory) {
            this.Attributes = attributeFactory.Invoke().ToArray();
            
            return this;
        }

        public ILookupControlBuilder ConfigureLookupControls() {
            this.LookupControlBuilder = new LookupControlBuilder();

            return this.LookupControlBuilder;
        }

        public CreateRequest Build() {
            var createRequest = new CreateRequest(this.ObjectType, this.ContextName, this.ParentPath, this.Attributes);

            if (this.LookupControlBuilder != null)
                createRequest.LookupControl = this.LookupControlBuilder.Build();

            if (this.RequestFilters.Count() >= 1)
                createRequest.RequestFilters = this.RequestFilters;

            return createRequest;
        }
    }
}