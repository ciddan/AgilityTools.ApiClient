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
    }

    public class ReturnAllAttributesFilter : ISearchRequestFilter, ILookupRequestFilter
    {
        private readonly bool _returnAllAttributes;

        public ReturnAllAttributesFilter(bool returnAllAttributes) {
            _returnAllAttributes = returnAllAttributes;
        }

        public XAttribute ToAdsml() {
            return new XAttribute("returnAllAttributes", _returnAllAttributes);
        }

        public void Validate() {
        }
    }

    public class AllowPagingFilter : ISearchRequestFilter, ILookupRequestFilter
    {
        private readonly bool _allowPaging;

        public AllowPagingFilter(bool allowPaging) {
            _allowPaging = allowPaging;
        }

        public XAttribute ToAdsml() {
            return new XAttribute("allowPaging", _allowPaging);
        }

        public void Validate() {
        }
    }

    public class PageSizeFilter : ISearchRequestFilter, ILookupRequestFilter
    {
        private readonly int _pageSize;

        public PageSizeFilter(int pageSize) {
            _pageSize = pageSize;
        }

        public XAttribute ToAdsml() {
            this.Validate();
            return new XAttribute("pageSize", _pageSize);
        }

        public void Validate() {
            if (this._pageSize <= 0)
                throw new ApiSerializationValidationException("PageSize must be larger than 0.");
        }
    }

    public class CountLimitFilter : ISearchRequestFilter, ILookupRequestFilter
    {
        private readonly int _countLimit;

        public CountLimitFilter(int countLimit) {
            _countLimit = countLimit;
        }

        public XAttribute ToAdsml() {
            this.Validate();
            return new XAttribute("countLimit", _countLimit);
        }

        public void Validate() {
            if (this._countLimit <= 0)
                throw new ApiSerializationValidationException("countLimit must be larger than 0.");
        }
    }

    public class ExcludeBinFilter : ISearchRequestFilter, ILookupRequestFilter
    {
        private readonly bool _excludeBin;

        public ExcludeBinFilter(bool excludeBin) {
            _excludeBin = excludeBin;
        }

        public XAttribute ToAdsml() {
            return new XAttribute("excludeBin", _excludeBin);
        }

        public void Validate() {
        }
    }

    public class ExcludeDocumentFilter : ISearchRequestFilter, ILookupRequestFilter
    {
        private readonly bool _excludeDocument;

        public ExcludeDocumentFilter(bool excludeDocument) {
            _excludeDocument = excludeDocument;
        }

        public XAttribute ToAdsml() {
            return new XAttribute("excludeDocument", _excludeDocument);
        }

        public void Validate() {
        }
    }
}