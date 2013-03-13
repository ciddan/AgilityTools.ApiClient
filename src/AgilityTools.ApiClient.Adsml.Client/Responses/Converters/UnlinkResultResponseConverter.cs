using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
  public class UnlinkResultResponseConverter : ResponseConverter<UnlinkResponse>
  {
    /// <summary>
    /// Constructor.
    /// </summary>
    public UnlinkResultResponseConverter(string validationDocument) : base(validationDocument, "UnlinkResponse") { }

    /// <summary>
    /// Converts the <see cref="XElement"/> into a <see cref="UnlinkResponse"/>.
    /// </summary>
    /// <param name="source">Required. The response to convert.</param>
    /// <returns>An <see cref="UnlinkResponse"/>.</returns>
    protected override UnlinkResponse ConvertSingle(XElement source) {
      return new UnlinkResponse {
        Code = (string)source.Attribute("code"),
        Description = (string)source.Attribute("description"),
        Messages = source.Descendants("Message").Select(x => x.Value).ToList()
      };
    }
  }
}