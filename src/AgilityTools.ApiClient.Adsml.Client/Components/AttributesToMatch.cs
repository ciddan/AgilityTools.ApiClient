using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
  public class AttributesToMatch : IAdsmlSerializable<XElement>
  {
    private readonly IAdsmlAttribute[] attributesToMatch;

    public AttributesToMatch(params IAdsmlAttribute[] attributesToMatch) {
      this.attributesToMatch = attributesToMatch;
    }

    public XElement ToAdsml() {
      return 
        new XElement("AttributesToMatch",
           this.attributesToMatch.Select(atm => atm.ToAdsml())
        );
    }
  }
}
