using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public class CompositeValue : IAdsmlSerializable<XElement>
    {
        public IList<Field> Fields { get; set; }

        public CompositeValue() {
            Fields = new List<Field>();
        }

        public XElement ToAdsml() {
            return 
                new XElement("CompositeValue", 
                    this.Fields.Select(cf => cf.ToAdsml()));
        }
    }
}