using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
  public class DeleteResultResponseConverter : ResponseConverter<DeleteResponse>
  {
    /// <summary>
    /// Constructor.
    /// </summary>
    public DeleteResultResponseConverter(string validationDocument) : base(validationDocument, "DeleteResponse") {}

    protected override DeleteResponse ConvertSingle(XElement source) {
      return new DeleteResponse {
        Code = (string)source.Attribute("code"),
        Description = (string)source.Attribute("description"),
        Messages = source.Descendants("Message").Select(x => x.Value).ToList()
      };
    }
  }
}