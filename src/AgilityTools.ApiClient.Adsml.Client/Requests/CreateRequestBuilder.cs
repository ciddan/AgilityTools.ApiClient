using System.Collections.Generic;
using System.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
    public class CreateRequestBuilder : ICreateRequestBuilder
    {
        internal string ContextName { get; private set; }
        internal string ObjectType { get; private set; }
        internal bool FailOnErrorFlag { get; private set; }
        internal StructureAttribute[] Attributes { get; private set; }
        internal LookupControlBuilder LookupControlBuilder { get; private set; }

        public IOTTCreateIFOErrorIATSet NewContextName(string contextName) {
            this.ContextName = contextName;

            return this; 
        }

        public IFOErrorIATSet ObjectTypeToCreate(string objectType) {
            this.ObjectType = objectType;
            
            return this;
        }

        public IAttributesToSet FailOnError() {
            this.FailOnErrorFlag = true;

            return this;
        }

        public IConfigLookupControls AttributesToSet(IList<StructureAttribute> structureAttributes) {
            this.Attributes = structureAttributes.ToArray();

            return this;
        }

        public IConfigLookupControls AttributesToSet(params StructureAttribute[] structureAttributes) {
            this.Attributes = structureAttributes;

            return this;
        }

        public ILookupControlBuilder ConfigureLookupControls() {
            this.LookupControlBuilder = new LookupControlBuilder();

            return this.LookupControlBuilder;
        }

        public CreateRequest Build() {
            return new CreateRequest(this.ObjectType, this.ContextName, this.Attributes);
        }
    }
}