using System;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace AgilityTools.ApiClient.Adsml.Client.Helpers
{
    public static class Extensions
    {
        public static string GetStringValue(this Enum value) {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            var attribs = fieldInfo.GetCustomAttributes(
                typeof (StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }

        public static bool IsNullOrEmpty(this string str) {
            return string.IsNullOrEmpty(str);
        }

        public static string SanitizeContextName(this string str) {
            if (str == null)
                return null;

            if (str == string.Empty)
                return string.Empty;

            return str.Replace("/", "@fs:");
        }

        public static void ValidateAdsmlResponse(this XDocument source) {
            if (source == null) {
                throw new ArgumentNullException("source");
            }

            var schemaSet = new XmlSchemaSet();
            schemaSet.Add("", XmlReader.Create("adsml.xsd"));

            source.Validate(schemaSet,
                            (sender, e) => {
                                if (e != null)
                                    throw new InvalidOperationException("Not a valid Adsml response.", e.Exception);
                            });
        }

        public static void ValidateAdsmlResponse(this XElement source) {
            if (source == null) {
                throw new ArgumentNullException("source");
            }

            var doc = XDocument.Parse(source.ToString());

            doc.ValidateAdsmlResponse();
        }

        public static string Capitalize(this string str, bool capitalizeAllWords = false) {
            if (str == null) {
                return null;
            }

            if (str == string.Empty) {
                return string.Empty;
            }

            if (!capitalizeAllWords) {
                return str.Substring(0, 1).ToUpper() + str.Substring(1);
            }

            string[] parts = str.Split(' ');

            string result =
                parts.Aggregate(string.Empty,
                                (current, part) =>
                                current + (part.Substring(0, 1).ToUpper() + part.Substring(1) + " "));

            return result.TrimEnd(' ');
        }
    }
}