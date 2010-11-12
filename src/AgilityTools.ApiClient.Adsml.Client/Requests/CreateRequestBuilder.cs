using System;
using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
    public class CreateRequestBuilder : ICreateRequestBuilder
    {
        public IOTTCreateIFOErrorIATSet NewContextName(string contextName) {
            return this;
        }

        public IFOErrorIATSet ObjectTypeToCreate(string objectType) {
            return this;
        }

        public IAttributesToSet FailOnError() {
            return this;
        }

        public void AttributesToSet(IList<StructureAttribute> structureAttributes) {
            
        }

        public void AttributesToSet(params StructureAttribute[] structureAttributes) {
            
        }

        public CreateRequest Build() {
            throw new NotImplementedException();
        }
    }
}