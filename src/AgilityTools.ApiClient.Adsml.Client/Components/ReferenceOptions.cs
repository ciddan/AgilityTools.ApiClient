using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public static class ReferenceOptions
    {
        public static UseChannelFilter UseChannel(int channelId) {
            return new UseChannelFilter(channelId);
        }

        public static ResolveAttributesFilter ResolveAttributes(bool resolve = true) {
            return new ResolveAttributesFilter(resolve);
        }

        public static ResolveSpecialCharactersFilter ResolveSpecialCharacters(bool resolve = true) {
            return new ResolveSpecialCharactersFilter(resolve);
        }

        public static ResolvePricesFilter ResolvePrices(bool resolve = true) {
            return new ResolvePricesFilter(resolve);
        }

        public static ResolvePriceFieldsFilter ResolvePriceFields(bool resolve = true) {
            return new ResolvePriceFieldsFilter(resolve);
        }

        public static ReturnValueOnlyFilter ReturnValuesOnly(bool valuesOnly = true) {
            return new ReturnValueOnlyFilter(valuesOnly);
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

    public class ResolvePricesFilter : IReferenceOptions
    {
        private readonly bool _resolve;

        internal ResolvePricesFilter(bool resolve) {
            _resolve = resolve;
        }

        public XAttribute ToAdsml() {
            return new XAttribute("resolvePrices", _resolve);
        }
    }

    public class ResolvePriceFieldsFilter : IReferenceOptions
    {
        private readonly bool _resolve;

        internal ResolvePriceFieldsFilter(bool resolve) {
            _resolve = resolve;
        }

        public XAttribute ToAdsml() {
            return new XAttribute("resolvePriceFields", _resolve);
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