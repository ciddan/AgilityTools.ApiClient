using System;
using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
    public class AqlQueryBuilder : IAqlQueryBuilder
    {
        internal string Path { get; private set; }
        internal QueryTypes SelectedQueryType { get; private set; }
        internal int ObjectTypeId { get; private set; }
        internal string ObjectTypeName { get; private set; }
        internal string Query { get; private set; }
        internal SearchControlBuilder SearchControlBuilder { get; private set; }

        public IQTypeIOTTFindIQStringICSControls BasePath(string path) {
            if (path == null) 
                throw new ArgumentNullException("path");

            this.Path = path;

            return this;
        }

        public IOTTFindIQStringICSControls QueryType(QueryTypes type) {
            this.SelectedQueryType = type;
            
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
            var objectTypeToFind =
                !string.IsNullOrEmpty(ObjectTypeName)
                    ? new IdNameReference(ObjectTypeName)
                    : new IdNameReference(ObjectTypeId);

            var searchControl =
                this.SearchControlBuilder != null
                    ? this.SearchControlBuilder.Build()
                    : null;

            return new AqlSearchRequest(this.Path, this.SelectedQueryType, objectTypeToFind, this.Query, searchControl);
        }
    }
}