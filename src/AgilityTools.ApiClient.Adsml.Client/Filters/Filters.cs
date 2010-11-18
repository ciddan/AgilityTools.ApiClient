using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public static class Filter
    {
        public static ExcludeBinFilter ExcludeBin() {
            return new ExcludeBinFilter(true);
        }

        public static ExcludeDocumentFilter ExcludeDocument() {
            return new ExcludeDocumentFilter(true);
        }

        public static ReturnAllAttributesFilter OmitStructureAttributes() {
            return new ReturnAllAttributesFilter(false);
        }

        public static AllowPagingFilter AllowPaging() {
            return new AllowPagingFilter(true);
        }

        public static PageSizeFilter PageSize(int pageSize) {
            return new PageSizeFilter(pageSize);
        }

        public static CountLimitFilter CountLimit(int countLimit) {
            return new CountLimitFilter(countLimit);
        }

        public static FailOnErrorFilter FailOnError() {
            return new FailOnErrorFilter(true);
        }

        public static ReturnRelationsAsAttributesFilter ReturnRelationsAsAttributes() {
            return new ReturnRelationsAsAttributesFilter(true);
        }
    }

    public class ReturnAllAttributesFilter : ISearchRequestFilter, ICreateRequestFilter
    {
        private readonly bool _returnAllAttributes;

        public ReturnAllAttributesFilter(bool returnAllAttributes) {
            _returnAllAttributes = returnAllAttributes;
        }

        public XAttribute ToAdsml() {
            return new XAttribute("returnAllAttributes", _returnAllAttributes);
        }
    }

    public class FailOnErrorFilter : ICreateRequestFilter
    {
        private readonly bool _failOnError;

        public FailOnErrorFilter(bool failOnError) {
            _failOnError = failOnError;
        }

        public XAttribute ToAdsml() {
            return new XAttribute("failOnError", _failOnError);
        }
    }

    public class AllowPagingFilter : ISearchRequestFilter
    {
        private readonly bool _allowPaging;

        public AllowPagingFilter(bool allowPaging) {
            _allowPaging = allowPaging;
        }

        public XAttribute ToAdsml() {
            return new XAttribute("allowPaging", _allowPaging);
        }
    }

    public class PageSizeFilter : ISearchRequestFilter
    {
        private readonly int _pageSize;

        public PageSizeFilter(int pageSize) {
            _pageSize = pageSize;
        }

        public XAttribute ToAdsml() {
            this.Validate();
            return new XAttribute("pageSize", _pageSize);
        }

        internal void Validate() {
            if (this._pageSize <= 0)
                throw new ApiSerializationValidationException("PageSize must be larger than 0.");
        }
    }

    public class CountLimitFilter : ISearchRequestFilter
    {
        private readonly int _countLimit;

        public CountLimitFilter(int countLimit) {
            _countLimit = countLimit;
        }

        public XAttribute ToAdsml() {
            this.Validate();
            return new XAttribute("countLimit", _countLimit);
        }

        internal void Validate() {
            if (this._countLimit <= 0)
                throw new ApiSerializationValidationException("countLimit must be larger than 0.");
        }
    }

    public class ExcludeBinFilter : ISearchRequestFilter
    {
        private readonly bool _excludeBin;

        public ExcludeBinFilter(bool excludeBin) {
            _excludeBin = excludeBin;
        }

        public XAttribute ToAdsml() {
            return new XAttribute("excludeBin", _excludeBin);
        }
    }

    public class ExcludeDocumentFilter : ISearchRequestFilter
    {
        private readonly bool _excludeDocument;

        public ExcludeDocumentFilter(bool excludeDocument) {
            _excludeDocument = excludeDocument;
        }

        public XAttribute ToAdsml() {
            return new XAttribute("excludeDocument", _excludeDocument);
        }
    }

    public class ReturnRelationsAsAttributesFilter : ILookupRequestFilter
    {
        private readonly bool _returnAsAttributes;

        public ReturnRelationsAsAttributesFilter(bool returnAsAttributes) {
            _returnAsAttributes = returnAsAttributes;
        }

        public XAttribute ToAdsml() {
            return new XAttribute("returnRelationsAsAttributes", _returnAsAttributes);
        }
    }
}