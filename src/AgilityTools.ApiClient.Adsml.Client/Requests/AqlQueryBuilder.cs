using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
    public class AqlQueryBuilder : IAqlQueryBuilder
    {
        internal string Path { get; private set; }
        internal AqlQueryTypes SelectedAqlQueryType { get; private set; }
        internal int ObjectTypeId { get; private set; }
        internal string ObjectTypeName { get; private set; }
        internal string Query { get; private set; }
        internal SearchControlBuilder SearchControlBuilder { get; private set; }

        public AqlQueryBuilder() {
            this.ObjectTypeId = -1;
        }

        public IQTypeIOTTFindIQStringICSControls BasePath(string path) {
            this.Path = path;

            return this;
        }

        public IOTTFindIQStringICSControls QueryType(AqlQueryTypes type) {
            this.SelectedAqlQueryType = type;
            
            return this;
        }

        public IQStringICSControls ObjectTypeToFind(int typeId) {
            this.ObjectTypeId = typeId;

            return this;
        }

        public IQStringICSControls ObjectTypeToFind(string typeName) {
            this.ObjectTypeName = typeName;

            return this;
        }

        public IConfigSearchControls QueryString(string query) {
            this.Query = query;

            return this;
        }

        public ISearchControlBuilder ConfigureSearchControls() {
            this.SearchControlBuilder = new SearchControlBuilder();

            return this.SearchControlBuilder;
        }

        public AqlSearchRequest Build() {
            IdNameReference objectTypeToFind = null;

            if (!string.IsNullOrEmpty(this.ObjectTypeName))
                objectTypeToFind = new IdNameReference(this.ObjectTypeName);

            if (this.ObjectTypeId != -1)
                objectTypeToFind = new IdNameReference(this.ObjectTypeId);

            var searchControl =
                this.SearchControlBuilder != null
                    ? this.SearchControlBuilder.Build()
                    : null;

            return new AqlSearchRequest(this.Path, this.SelectedAqlQueryType, objectTypeToFind, this.Query, searchControl);
        }
    }
}