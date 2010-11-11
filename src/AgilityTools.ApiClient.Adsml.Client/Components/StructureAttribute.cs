using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class StructureAttribute : IAdsmlSerializable<XElement> {
        public int DefinitionId { get; set; }
        public string Name { get; set; }
        public IList<StructureValue> Values { get; set; }

	    public StructureAttribute() : this(new List<StructureValue>()) {
	    }

	    public StructureAttribute(IList<StructureValue> values){
		    this.Values = values;
	    }

	    public XElement ToAdsml() {
		    this.Validate();

	        return !string.IsNullOrEmpty(this.Name)
	                   ? new XElement("StructureAttribute",
	                                  new XAttribute("id", this.DefinitionId),
	                                  new XAttribute("name", this.Name),
	                                  this.Values.Select(val => val.ToAdsml()).ToList())
	                   : new XElement("StructureAttribute",
	                                  new XAttribute("id", this.DefinitionId),
	                                  this.Values.Select(val => val.ToAdsml()).ToList());
	    }

        private void Validate() {
		    if (this.DefinitionId == 0)
                throw new ApiSerializationValidationException("DefinitionId has to be set.");
		
		    if (this.Values.Count() < 1)
                throw new ApiSerializationValidationException("At least one StructureAttribute Value must be specified.");
	    }
    }
}