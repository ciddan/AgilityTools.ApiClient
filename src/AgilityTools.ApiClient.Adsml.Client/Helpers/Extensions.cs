using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace AgilityTools.ApiClient.Adsml.Client
{
  public static class Extensions
  {
    /// <summary>
    /// Gets the <see cref="StringValueAttribute"/> associated with an enum member.
    /// </summary>
    /// <param name="value">The enum whose string value to get.</param>
    /// <returns></returns>
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

    /// <summary>
    /// Conveniance method to make a string IsNullOrEmpty check more syntactically logical.
    /// </summary>
    /// <example><code>
    /// string foo = "foo";
    /// 
    /// if (foo.IsNullOrEmpty())
    /// </code></example>
    /// <param name="str">The string to check.</param>
    /// <returns>A boolean indicating if the string is either null or empty.</returns>
    public static bool IsNullOrEmpty(this string str) {
      return string.IsNullOrEmpty(str);
    }

    /// <summary>
    /// Replaces any "/" with "@fs:" for compatibility with ADMSL.
    /// </summary>
    /// <param name="str">Required. The string to sanitize.</param>
    /// <returns><see cref="string"/>.</returns>
    public static string SanitizeContextName(this string str) {
      if (str == null)
        return null;

      return str == string.Empty
               ? string.Empty
               : str.Replace("/", "@fs:");
    }

    /// <summary>
    /// Validates an <see cref="XDocument"/> against the adsml language definition adsml.xsd. The xsd should be placed in the same directory as the executing assembly.
    /// </summary>
    /// <param name="source">Required. The document to validate.</param>
    /// <param name="xsdValidator">Required. Relative or full path to the Agility ADMSL API definition file, adsml.xsd.</param>
    private static void ValidateAdsmlResponse(this XDocument source, string xsdValidator) {
      if (source == null) throw new ArgumentNullException("source");

      var schemaSet = new XmlSchemaSet();
      schemaSet.Add("", XmlReader.Create(xsdValidator));

      source.Validate(
        schemaSet,
        (sender, e) => {
          if (e != null)
            throw new InvalidOperationException("Not a valid ADSML document.", e.Exception);
        }
      );
    }

    /// <summary>
    /// Validates an <see cref="XElement"/> against the adsml.xsd definition file. The xsd should be placed in the same directory as the executing assembly.
    /// </summary>
    /// <param name="source">Required. The <see cref="XElement"/> to validate.</param>
    /// <param name="xsdValidator">Required. Relative or full path to the Agility ADMSL API definition file, adsml.xsd.</param>
    public static void ValidateAdsmlResponse(this XElement source, string xsdValidator) {
      if (source == null) throw new ArgumentNullException("source");
      var doc = XDocument.Parse(source.ToString());

      doc.ValidateAdsmlResponse(xsdValidator);
    }

    /// <summary>
    /// Capitalizes either the first or all words in the string.
    /// </summary>
    /// <param name="str">The string to capitalize.</param>
    /// <param name="capitalizeAllWords">Optional.</param>
    /// <returns>A string with the specified mode of capitalization.</returns>
    public static string Capitalize(this string str, bool capitalizeAllWords = false) {
      if (str == null) return null;
      if (str == string.Empty) return string.Empty;

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

    /// <summary>
    /// Gets the full path to the executing assembly.
    /// </summary>
    /// <param name="assembly">Required. The <see cref="Assembly"/> whose path to get.</param>
    /// <returns>A string with the full path to the <paramref name="assembly"/>.</returns>
    private static string GetAssemblyPath(this Assembly assembly) {
      string codeBase = assembly.CodeBase;

      var uri = new UriBuilder(codeBase);
      string path = Uri.UnescapeDataString(uri.Path);

      return Path.GetDirectoryName(path);
    }
  }
}