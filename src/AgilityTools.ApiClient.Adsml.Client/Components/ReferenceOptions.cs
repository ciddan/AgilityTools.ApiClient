using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// A static class that contains static factory methods for creating various objects that implement <see cref="IReferenceOptions"/>.
    /// </summary>
    public static class ReferenceOptions
    {
        /// <summary>
        /// Used to tell the API which content channel to use for reference resolution.
        /// </summary>
        /// <param name="channelId">The id of the channel to use for resolution.</param>
        /// <returns><see cref="UseChannelFilter"/> with the channel id set.</returns>
        public static UseChannelFilter UseChannel(int channelId) {
            return new UseChannelFilter(channelId);
        }

        /// <summary>
        /// Tells the API whether or not to resolve attribute references to data.
        /// </summary>
        /// <param name="resolve">Optional. Defaults to true.</param>
        /// <returns><see cref="ResolveAttributesFilter"/>.</returns>
        public static ResolveAttributesFilter ResolveAttributes(bool resolve = true) {
            return new ResolveAttributesFilter(resolve);
        }

        /// <summary>
        /// Tells the API whether or not to resolve special character references.
        /// </summary>
        /// <param name="resolve">Optional. Defaults to true.</param>
        /// <returns><see cref="ResolveSpecialCharactersFilter"/>.</returns>
        public static ResolveSpecialCharactersFilter ResolveSpecialCharacters(bool resolve = true) {
            return new ResolveSpecialCharactersFilter(resolve);
        }

        /// <summary>
        /// Tells the API whether or not to resolve price references.
        /// </summary>
        /// <param name="resolve">Optional. Defaults to true.</param>
        /// <returns><see cref="ResolvePricesFilter"/>.</returns>
        public static ResolvePricesFilter ResolvePrices(bool resolve = true) {
            return new ResolvePricesFilter(resolve);
        }

        /// <summary>
        /// Tells the API whether or not to resolve price field references.
        /// </summary>
        /// <param name="resolve">Optional. Defaults to true.</param>
        /// <returns><see cref="ResolvePriceFieldsFilter"/>.</returns>
        public static ResolvePriceFieldsFilter ResolvePriceFields(bool resolve = true) {
            return new ResolvePriceFieldsFilter(resolve);
        }

        /// <summary>
        /// Tells the API whether or not to keep the reference tags when resolving references. If false, the result will contain references intersperced with clear text data.
        /// </summary>
        /// <param name="valuesOnly">Optional. Defaults to true.</param>
        /// <returns><see cref="ReturnValueOnlyFilter"/>.</returns>
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