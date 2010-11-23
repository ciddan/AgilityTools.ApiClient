using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components.Attributes
{
    public class StructureAttribute : IAdsmlAttribute<XElement>
    {
        public int DefinitionId { get; set; }
        public string Name { get; set; }
        public IList<StructureValue> Values { get; set; }

	    public StructureAttribute() : this(new List<StructureValue>()) {
	    }

	    public StructureAttribute(IList<StructureValue> values){
		    this.Values = values;
	    }

        public StructureAttribute(int definitionId, IList<StructureValue> values) 
            : this (null, definitionId, values) { }

        public StructureAttribute(string name, IList<StructureValue> values) 
            : this (name, 0, values) { }

        public StructureAttribute(string name, int definitionId, IList<StructureValue> values) {
            this.Name = name;
            this.DefinitionId = definitionId;
            this.Values = values;
        }

	    public XElement ToAdsml() {
		    this.Validate();

	        var element = new XElement("StructureAttribute");

            if (this.DefinitionId != 0)
                element.Add(new XAttribute("id", this.DefinitionId));

            if (!string.IsNullOrEmpty(this.Name))
                element.Add(new XAttribute("name", this.Name));

            if (this.Values != null && this.Values.Count() >= 1)
                element.Add(this.Values.Select(val => val.ToAdsml()));

	        return element;
	    }

        private void Validate() {
		    if (this.DefinitionId == 0 && string.IsNullOrEmpty(this.Name))
                throw new ApiSerializationValidationException("DefinitionId or Name has to be set.");
	    }

        public static StructureAttribute New(string name, params StructureValue[] values) {
            return new StructureAttribute(name, values);
        }

        public static StructureAttribute New(int definitionId, params StructureValue[] values) {
            return new StructureAttribute(definitionId, values);
        }

        public static StructureAttribute New(string name, int definitionId, params StructureValue[] values)
        {
            return new StructureAttribute(name, definitionId, values);
        }
    }
}