using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public class CompositeAttribute : IAdsmlAttribute
    {
        public string Name { get; set; }
        public IList<CompositeValue> CompositeValues { get; set; }

        public CompositeAttribute() {
            this.CompositeValues = new List<CompositeValue>();
        }

        public string GetName() {
            return this.Name;
        }

        public IEnumerable<AttributeValue> GetValues() {
            throw new NotSupportedException();
        }

        public bool HasValues() {
            return this.CompositeValues.Count > 0;
        }

        public XElement ToAdsml() {
            if (string.IsNullOrEmpty(this.Name)) {
                throw new InvalidOperationException("Name not set.");
            }

            return new XElement("CompositeAttribute", new XAttribute("name", this.Name), CompositeValues.Select(f => f.ToAdsml()));
        }
    }
}