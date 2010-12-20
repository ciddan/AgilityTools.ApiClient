using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components.Attributes
{
    public class CompositeAttribute : IAdsmlAttribute
    {
        public string Name { get; set; }
        public IList<CompositeValue> Fields { get; set; }

        public CompositeAttribute() {
            this.Fields = new List<CompositeValue>();
        }

        public XElement ToAdsml() {
            if (string.IsNullOrEmpty(this.Name)) {
                throw new InvalidOperationException("Name not set.");
            }

            return new XElement(this.Name, Fields.Select(f => f.ToAdsml()));
        }
    }
}