using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public interface ISearchRequestFilter
    {
        XAttribute ToApiXml();
    }

    public static class Filter
    {
        public static ExcludeBinFilter ExcludeBin() {
            return new ExcludeBinFilter(true);
        }

        public static ExcludeDocumentFilter ExcludeDocument() {
            return new ExcludeDocumentFilter(true);
        }
    }

    public class ExcludeBinFilter : ISearchRequestFilter
    {
        private readonly bool _excludeBin;

        public ExcludeBinFilter(bool excludeBin) {
            _excludeBin = excludeBin;
        }

        public XAttribute ToApiXml() {
            return new XAttribute("excludeBin", _excludeBin);
        }
    }

    public class ExcludeDocumentFilter : ISearchRequestFilter
    {
        private readonly bool _excludeDocument;

        public ExcludeDocumentFilter(bool excludeDocument) {
            _excludeDocument = excludeDocument;
        }

        public XAttribute ToApiXml() {
            return new XAttribute("excludeDocument", _excludeDocument);
        }
    }
}