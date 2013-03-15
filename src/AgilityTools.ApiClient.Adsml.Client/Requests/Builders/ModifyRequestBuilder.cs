using System;
using System.Collections.Generic;
using System.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
    public class ModifyRequestBuilder : IModifyRequestBuilder
    {
        internal IList<IModifyRequestFilter> RequestFilters { get; private set; }
        internal List<ModificationItem> Modifications { get; private set; }

        internal string ContextToModify { get; private set; }
        internal LookupControlBuilder LookupControlBuilder { get; private set; }

        public ModifyRequestBuilder() {
            this.RequestFilters = new List<IModifyRequestFilter>();
            this.Modifications = new List<ModificationItem>();
        }

        public IReturnNoAttributesFailOnErrorAddModificationsConfigLookupControls Context(string contextToModify) {
            this.ContextToModify = contextToModify;

            return this;
        }

        public IFailOnErrorAddModificationsConfigLookupControls ReturnNoAttributes() {
            this.RequestFilters.Add(Filter.ReturnNoAttributes());

            return this;
        }

        public IAddModificationsConfigLookupControls FailOnError() {
            this.RequestFilters.Add(Filter.FailOnError());

            return this;
        }

        public IAddModificationsConfigLookupControls AddModification<TAttribute>(Modifications modificationType, TAttribute attribute) 
        where TAttribute : class, IAdsmlAttribute {
            var modification = ModificationItem.New(modificationType, attribute);

            this.Modifications.Add(modification);

            return this;
        }

        public IAddModificationsConfigLookupControls AddModifications(Func<IList<ModificationItem>> modificationsFactory) {
            this.Modifications.AddRange(modificationsFactory.Invoke());

            return this;
        }

        public ILookupControlBuilder ConfigureLookupControls() {
            this.LookupControlBuilder = new LookupControlBuilder();

            return this.LookupControlBuilder;
        }

        public ModifyRequest Build() {
            var modifyRequest = new ModifyRequest(this.ContextToModify, this.Modifications);

            if (this.LookupControlBuilder != null)
                modifyRequest.LookupControl = this.LookupControlBuilder.Build();

            if (this.RequestFilters.Count() >= 1)
                modifyRequest.RequestFilters = this.RequestFilters;

            return modifyRequest; 
        }
    }
}