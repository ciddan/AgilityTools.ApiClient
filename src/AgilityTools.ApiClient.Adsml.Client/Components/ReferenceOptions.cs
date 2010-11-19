using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public static class ReferenceOptions
    {
        public static UseChannelFilter UseChannel(int channelId) {
            return new UseChannelFilter(channelId);
        }

        public static ResolveAttributesFilter ResolveAttributes() {
            return new ResolveAttributesFilter(true);
        }

        public static ResolveSpecialCharactersFilter ResolveSpecialCharacters() {
            return new ResolveSpecialCharactersFilter(true);
        }

        public static ReturnValueOnlyFilter ReturnValuesOnly() {
            return new ReturnValueOnlyFilter(true);
        }
    }

    public class ResolveAttributesFilter : IReferenceOptions
    {
        private readonly bool _resolve;

        internal ResolveAttributesFilter(bool resolve) {
            _resolve = resolve;
        }

        public XAttribute ToAdsml() {
            return new XAttribute("resolveAttributes", _resolve);
        }
    }

    public class ResolveSpecialCharactersFilter : IReferenceOptions
    {
        private readonly bool _resolve;

        internal ResolveSpecialCharactersFilter(bool resolve) {
            _resolve = resolve;
        }

        public XAttribute ToAdsml() {
            return new XAttribute("resolveSpecialCharacters", _resolve);
        }
    }

    public class ReturnValueOnlyFilter : IReferenceOptions
    {
        private readonly bool _resolve;

        internal ReturnValueOnlyFilter(bool resolve) {
            _resolve = resolve;
        }

        public XAttribute ToAdsml() {
            return new XAttribute("valueOnly", _resolve);
        }
    }

    public class UseChannelFilter : IReferenceOptions
    {
        private readonly int _channelId;

        internal UseChannelFilter(int channelId) {
            _channelId = channelId;
        }

        public XAttribute ToAdsml() {
            return new XAttribute("channelId", _channelId.ToString());
        }
    }
}