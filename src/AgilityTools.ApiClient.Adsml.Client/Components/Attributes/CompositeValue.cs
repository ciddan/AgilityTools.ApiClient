using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Represents an Adsml CompositeValue. Implements <see cref="IAdsmlSerializable{XElement}"/>
    /// </summary>
    public class CompositeValue : IAdsmlSerializable<XElement>
    {
        public IList<Field> Fields { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public CompositeValue() {
            Fields = new List<Field>();
        }

        /// <summary>
        /// Converts the instance to an <see cref="XElement"/> Adsml represenation.
        /// </summary>
        /// <returns><see cref="XElement"/></returns>
        public XElement ToAdsml() {
            return 
                new XElement("CompositeValue", 
                    this.Fields.Select(cf => cf.ToAdsml()));
        }
    }
}